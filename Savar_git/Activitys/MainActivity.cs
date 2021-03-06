﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Maps;
using System.Collections.Generic;
using System.Threading;
using Android.Locations;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Android.Gms.Maps.Model;

namespace Savar_git
{
    [Activity(Label = "MapScreen", MainLauncher = false)]
    
    public class MapScreen : Activity, IOnMapReadyCallback, ILocationListener
    {
        private IList<string> ListParam;
        private string _ProviderGps;
        private string cLog;
        private string _ScreenCall;
        private Location _currentLocation;
        private LocationManager _locationManager;
        private double PosX, PosY;
        private Semaphore AcessoAoBanco = new Semaphore(1,2);
        private Thread UpdateStatus ;
        private OnibusClass ObjOnibus = new OnibusClass();
        private RotaClass ObjRota = new RotaClass();
        private volatile bool _ContinueTread;
        
        struct Positions
        {
            public double PosX { get; set; }
            public double PosY { get; set; }
            public string IdPonto { get; set; }
            public string Descricao { get; set; }
        }
        
        // ######## - Metodos IOnMapReadyCallback 
        public void OnMapReady(GoogleMap googleMap)
        {
            //CriaPontos(googleMap);
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            new PontosClass().CarregaPontosMapa(googleMap);
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());
        }
        // ######## - Metodos IOnMapReadyCallback fim
        // ######## - Metodos do iLocationListner 
        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                AcessoAoBanco.WaitOne();
                PosX = _currentLocation.Latitude;
                PosY = _currentLocation.Longitude;
                UpdateOnStatusChange();
                AcessoAoBanco.Release();
            }

        }

        public void OnProviderDisabled(string provider)
        {
            /* var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
             StartActivity(intent);*/
        }

        public void OnProviderEnabled(string provider)
        {

        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            if (status == Availability.Available)
            {
                _ProviderGps = provider;
                UpdateOnStatusChange();
            }
        }

        // ######## - Metodos do iLocationListner Fim 
        //------------------------------------------------------------------------------------
        // ######## - Status do APP
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            ListParam = Intent.GetStringArrayListExtra("Array") ;
            _ScreenCall = Intent.GetStringExtra("CallScreen") ?? "";
            if (ListParam != null)
            {

                
                _ContinueTread = true;
                UpdateStatus = new Thread(UpdatePositionFunc);
                UpdateStatus.Start();
                ObjRota.UpdateRota(ListParam[2].ToString(), "", ListParam[0]);
            }
         
            else if(_ScreenCall != "")
            {

            }
            Gps();
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.mapFrag);
            mapFragment.GetMapAsync(this);
            
        }

        protected override void OnResume()
        {
            base.OnResume();
            if(_ProviderGps != "")
            {
                _locationManager.RequestLocationUpdates(_ProviderGps, 0, 0, this);
            }
            
        }
        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
            StopThread();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopThread();
            if (ListParam != null)
            {
                new RotaClass().UpdateRota(ListParam[2], "", " ");
                new OnibusClass().UpdateOnibus(ListParam[0], ListParam[1], 0, 0, 0);
            }
        }
        // ######## - Status do APP Fim
        public void StopThread()
        {
            _ContinueTread = false;
        }
        void UpdatePositionFunc()
        {
            string cRet = "";
            
            string Hora = DateTime.Now.Hour.ToString();
            while (_ContinueTread)
            {
                if (_currentLocation != null)
                {
                    
                    cRet = ObjOnibus.UpdateOnibus(ListParam[0], ListParam[1], GetPosX(), GetPosY());
                    
                    Thread.Sleep(10000);// espera 10 segundos para executar de novo
                    if (cRet != "")
                    {
                        Console.WriteLine(cRet);
                        
                    }
                    //Console.WriteLine("Executou ----------------------------------");
                }
            }
        }
        void UpdateOnStatusChange()
        {
            string cRet = "";
            double PosiX, PosiY;
            OnibusClass ObjOnibus = new OnibusClass();
            PosiX = GetPosX();
            PosiY = GetPosY();
            cRet = ObjOnibus.UpdateOnibus(ListParam[0], ListParam[1], PosiX, PosiY);
            if(cRet != "")
            {
                Console.WriteLine( cRet);
            }
            //Console.WriteLine("Executou StatusChabge ----------------------------------");
        }
        private void Gps()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria CriteriosGPS = new Criteria { Accuracy = Accuracy.Fine };

            IList<string> ProvidersDisponiveis = _locationManager.GetProviders(CriteriosGPS, true);

            if (ProvidersDisponiveis.Any())
            {
                _ProviderGps = ProvidersDisponiveis.First();
            }
            else
            {
                _ProviderGps = string.Empty;
            }
            _currentLocation =  _locationManager.GetLastKnownLocation(_ProviderGps);
        }
        public double GetPosX()
        {
            double RetValor;
            AcessoAoBanco.WaitOne();
            RetValor = _currentLocation.Latitude;
            AcessoAoBanco.Release();
            return RetValor;
        }
        public double GetPosY()
        {
            double RetValor;
            AcessoAoBanco.WaitOne();
            RetValor = _currentLocation.Longitude;
            AcessoAoBanco.Release();
            return RetValor;
        }
        

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }
        private string DisplayAddress(Address address)
        {
            string cRet = "";
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                cRet = deviceAddress.ToString();
            }
            else
            {
                cLog += "\n\n Unable to determine the address. Try again in a few minutes.";
            }

            return cRet;
        }
    }
}

