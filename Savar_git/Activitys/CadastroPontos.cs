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

namespace Savar_git.Activitys
{
    
    [Activity(Label = "CadastroPontos", MainLauncher = true)]
    public class  CadastroPontos : Activity, IOnMapReadyCallback
    {
        protected MapFragment Map;
        protected Button Btn_AddPonto, Btn_SalvarPonto;
        protected Marker _NovoPonto;
        protected string _DescriPonto;
        protected double _PosXMark, _PosYMark;
        public void OnMapReady(GoogleMap googleMap)
        {
            //CriaPontos(googleMap);
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MapClick += GoogleMap_MapClick;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());
        }

        private void GoogleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            GoogleMap mapsGoogle = (GoogleMap)sender;
            
            _PosXMark = e.Point.Latitude;
            _PosYMark = e.Point.Longitude;
            if(_NovoPonto == null)
            {
                _NovoPonto = mapsGoogle.AddMarker(new MarkerOptions().SetPosition(new LatLng(_PosXMark, _PosYMark)));
            }
            else
            {
                _NovoPonto.Remove();
                _NovoPonto = mapsGoogle.AddMarker(new MarkerOptions().SetPosition(new LatLng(_PosXMark, _PosYMark)));
            }
            
            Btn_AddPonto.Enabled = true;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);
            Btn_AddPonto = FindViewById<Button>(Resource.Id.BtnAddPonto);
            Btn_SalvarPonto = FindViewById<Button>(Resource.Id.Btn_SalvarPonto);
            Map = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.mapFrag);

            Btn_SalvarPonto.Click += Btn_SalvarPonto_Click1;


            Map.GetMapAsync(this);
            Btn_AddPonto.Enabled = false;
            Btn_AddPonto.Click += Btn_Salvar_Click;
        }

        private void Btn_SalvarPonto_Click1(object sender, EventArgs e)
        {
            PontosClass Ponto = new PontosClass();
            GoogleMap mapsGoogle = (GoogleMap)sender;
            if (Ponto.InsertPonto(_NovoPonto.Title, _NovoPonto.Position.Latitude, _NovoPonto.Position.Longitude))
            {
                Toast.MakeText(this, "Ponto criado com sucesso",ToastLength.Long);
                Ponto.CarregaPontosMapa(mapsGoogle);
            }
            else
            {
                Toast.MakeText(this, "Erro. Tente novamente mais tarde", ToastLength.Long);
            }
        }

        private void Btn_SalvarPonto_Click(object sender, EventArgs e)
        {
            PontosClass MngPontos = new PontosClass();
            EditText Descricao = FindViewById<EditText>(Resource.Id.DescricaoPonto);
            _DescriPonto = Descricao.Text;

            if(MngPontos.InsertPonto(_DescriPonto, _PosXMark, _PosYMark))
            {
                Toast.MakeText(this,"Ponto criado com sucesso!",ToastLength.Long);
            }
            else
            {
                Toast.MakeText(this, "Erro na criação do ponto, Tente novamente mais tarde!!", ToastLength.Long);
            }
        }

        private void Btn_Salvar_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();

            PopUpPonto InfoPopUp = new PopUpPonto();
            InfoPopUp.Show(transaction, "testo");
        }
    }
    
}