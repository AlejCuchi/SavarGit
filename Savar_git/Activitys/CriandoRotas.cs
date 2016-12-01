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
namespace Savar_git
{
    [Activity(Label = "CriandoRotas")]
    public class CriandoRotas : Activity , IOnMapReadyCallback
    {
        private List<Marker> MarcasRota;
        private Button SalvarRota;
        private int NumMarcas =0 ;
        private RotaClass Rotas = new RotaClass();
        private string _IdRota, _DescricaoRota;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Main);

            
            SalvarRota = FindViewById<Button>(Resource.Id.BtnAddPonto);

            SalvarRota.Click += SalvarRota_Click;
            NumMarcas = 0;
            _IdRota = Intent.GetStringExtra("IdRota") ?? "";
            _DescricaoRota = Intent.GetStringExtra("Descricao") ?? "";
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.mapFrag);
            mapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            PontosClass Ponto = new PontosClass();
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomTo(googleMap.MaxZoomLevel));
            googleMap.MoveCamera(CameraUpdateFactory.NewLatLng(new LatLng(-26.3193264, -48.8553668)));
            googleMap.MarkerClick += GoogleMap_MarkerClick; 
            Ponto.CarregaPontosMapa(googleMap);
        }
        private void GoogleMap_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            Marker Marca = e.Marker;
            bool lTesta = false;
            if(MarcasRota != null)
            { 
                foreach (Marker Item in MarcasRota)
                {
                    if (Item.Title == Marca.Title)
                    { 
                    
                        lTesta = true;
                        break;
                    }
                }
                if (lTesta)
                {
                    Rotas.RemovePonto(Marca);
                    MarcasRota.Remove(Marca);
                    Marca.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.arrowdown));
                    NumMarcas--;
                }
                else
                {
                    Rotas.AddPonto(Marca);
                    MarcasRota.Add(Marca);
                    Marca.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.arrowcircle));
                    NumMarcas++;
                }
                if (NumMarcas > 1)
                {
                    SalvarRota.Enabled = true;
                }
                else
                {
                    SalvarRota.Enabled = false;
                }
            }
            else
            {
                MarcasRota = new List<Marker> { Marca};
            }
            
        }
        

        
        private void SalvarRota_Click(object sender, EventArgs e)
        {
            
        }
    }























    [Activity(Label = "InfoRotas", MainLauncher = true)]
    class PopUpRota : Activity
    {
        
        private string AddPonto;
        TextView TextFromPopUp;
        Button MandaPraTelaDoMapa;
        EditText ValorDIdDarota;
        EditText ValorDescricao;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CadastroRotasInicio);
            MandaPraTelaDoMapa = FindViewById<Button>(Resource.Id.SelecPontosRota);
            ValorDescricao = FindViewById<EditText>(Resource.Id.EdT_PontoDeDescricao);
            ValorDIdDarota = FindViewById<EditText>(Resource.Id.EdT_NumeroDarota);

            MandaPraTelaDoMapa.Click += MandaPraTelaDoMapa_Click;
        }

        private void MandaPraTelaDoMapa_Click(object sender, EventArgs e)
        {
            RotaClass RotasTeste = new RotaClass();
            if (ValorDescricao.Text !=""  && ValorDIdDarota.Text != "")
            {
                Intent TelaDoMapa = new Intent(this, typeof(CriandoRotas));
                Bundle ValorDaRota = new Bundle();
                ValorDaRota.PutString("IdRota", ValorDIdDarota.Text);
                ValorDaRota.PutString("Descricao", ValorDescricao.Text);
                StartActivity(TelaDoMapa);
            }
            else if (RotasTeste.ExistRota(ValorDIdDarota.Text))
            {
                Toast.MakeText(this, "Rota Já existe", ToastLength.Long);
            }
            else
            {
                Toast.MakeText(this, "Descrição ou ID da rota Invalido ", ToastLength.Long);

            }
        }

        public string IDRota()
        {
            return AddPonto;
        }

        private void Btn_SalvarPonto_Click(object sender, EventArgs e)
        {
            EditText IdRota = FindViewById<EditText>(Resource.Id.DescricaoPonto);
            if (new RotaClass().ExistRota(IdRota.Text))
            {
                TextFromPopUp.Text = "ROTA EXISTE:";
            }
            
        }
    }
}