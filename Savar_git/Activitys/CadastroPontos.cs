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
using Android.Gms.Maps.Model;
using Android.Locations;

namespace Savar_git.Activitys
{
    
    [Activity(Label = "CadastroPontos", MainLauncher = false)]
    public class  CadastroPontos : Activity, IOnMapReadyCallback
    {
        protected MapFragment Map;
        protected Button Btn_AddPonto, Btn_SalvarPonto;
        protected Marker _NovoPonto;
        protected Location _currentLocation;
        protected LocationManager _locationManager;
        private double _PosXMark, _PosYMark;

       
        public void OnMapReady(GoogleMap googleMap)
        {
            PontosClass Ponto = new PontosClass();
            
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MapClick += GoogleMap_MapClick;
            
            Ponto.CarregaPontosMapa(googleMap);
        }

        private void GoogleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            GoogleMap googleMap = (GoogleMap)sender;
            _PosXMark = e.Point.Latitude;
            _PosYMark = e.Point.Longitude;
            if(_NovoPonto == null)
            {
                _NovoPonto = googleMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(_PosXMark, _PosYMark)));
            }
            else
            {
                _NovoPonto.Remove();
                _NovoPonto = googleMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(_PosXMark, _PosYMark)));
            }
            Btn_AddPonto.Enabled = true;
            new PontosClass().CarregaPontosMapa(googleMap);
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);
            Btn_AddPonto = FindViewById<Button>(Resource.Id.BtnAddPonto);
            //Btn_SalvarPonto = FindViewById<Button>(Resource.Id.Btn_SalvarPonto);
            Map = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.mapFrag);
            Map.GetMapAsync(this);
            Btn_AddPonto.Enabled = false;
            Btn_AddPonto.Click += Btn_Salvar_Click;
            
        }
        private void Btn_Salvar_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            Bundle Args = new Bundle();
            PontosClass Ponto = new PontosClass();
            PopUpPonto InfoPopUp = new PopUpPonto();

            Args.PutDouble("Longitude", _NovoPonto.Position.Longitude);
            Args.PutDouble("Latitude", _NovoPonto.Position.Latitude);
            InfoPopUp.Arguments = Args;
            InfoPopUp.Show(transaction, "");
            Btn_AddPonto.Enabled = false;
        }

        
    }
    
}