
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Locations;
using System.Threading.Tasks;
using Android.Gms.Maps.Model;
using System.Threading;
using System.Data;

namespace Savar_git
{
    [Activity(Label = "MapaUser")]
    public class MapaUser : Activity, IOnMapReadyCallback, ILocationListener
    {
        LinearLayout BuscaRotas;
        Location _currentLocation;
        LocationManager _locationManager;
        string _ProviderGps;
        private string ID_Rota,NumeroOnibus;
        Thread AtualizaPositionOnibus;
        private GoogleMap Mapp;
        public Marker OnibusMark;
        private bool _KeepThread;
        protected override  void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Gps();
            SetContentView(Resource.Layout.Main);
            BuscaRotas = FindViewById<LinearLayout>(Resource.Id.LayoutBuscaBuss);
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.mapFrag);
            
            BuscaRotas.Visibility = ViewStates.Gone;
            ID_Rota = Intent.GetStringExtra("Rota");
            NumeroOnibus = Intent.GetStringExtra("Onibus");
            _KeepThread = true;
            AtualizaPositionOnibus = new Thread(AtualizaOnibusPosicao);
            //AtualizaPositionOnibus.Start();
            mapFragment.GetMapAsync(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ThreadStop();
        }
        void AtualizaOnibusPosicao()
        {
            
            while (_KeepThread)
            {
                
                if (OnibusMark != null)
                {
                    
                    
                }
                    Thread.Sleep(20000);

            }
        }

        void ThreadStop()
        {
            _KeepThread = false;
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
            _locationManager.RequestLocationUpdates(_ProviderGps,1000,200,this);
            _currentLocation = _locationManager.GetLastKnownLocation(_ProviderGps);
        }



        public void OnMapReady(GoogleMap googleMap)
        {
            OnibusClass AcompanhaOnibus = new OnibusClass();
            DataRow Onibus;
            string PosX, PosY;
            Double ValX, ValY;


            Onibus = AcompanhaOnibus.SelectOnibus(Convert.ToInt32(NumeroOnibus)).Rows[0];

            PosX = Onibus.ItemArray[2].ToString();
            PosY = Onibus.ItemArray[3].ToString();
            ValX = Convert.ToDouble(PosX);
            ValY = Convert.ToDouble(PosY);

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            new PontosClass().CarregaPontosMapa(googleMap);
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());
            if(_currentLocation == null)
            {
                Gps();
                _currentLocation = _locationManager.GetLastKnownLocation(_ProviderGps);
                
            }
            googleMap.MoveCamera(CameraUpdateFactory.NewCameraPosition(new
                                 CameraPosition(new LatLng(_currentLocation.Latitude, ValY), 35, 0, 0)));

            OnibusMark = googleMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(ValX, _currentLocation.Longitude))
                                                             .SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.bus)));
            Mapp = googleMap;
        }

        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}