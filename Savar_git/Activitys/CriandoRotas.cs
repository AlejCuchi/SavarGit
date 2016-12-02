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
    [Activity(Label = "CriandoRotas",MainLauncher = false) ]
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
            int nI;
            if(MarcasRota != null)
            { 
                for(nI= 0; nI< MarcasRota.Count;nI++)
                {
                    if (MarcasRota[nI].Title.Split('-')[0] == Marca.Title.Split('-')[0])
                    { 
                    
                        lTesta = true;
                        break;
                    }
                }
                if (lTesta)
                {

                    string ID_ponto = Marca.Title.Split('-')[0];
                    Rotas.RemovePonto(ID_ponto);
                    MarcasRota.RemoveRange(nI,1);
                    Marca.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.arrowdown));
                    NumMarcas--;
                }
                else
                {
                    string ID_ponto = Marca.Title.Split('-')[0];
                    Rotas.AddPonto(ID_ponto);
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
                string MarcaId = Marca.Title.Split('-')[0];
                MarcasRota = new List<Marker> { Marca };
                Rotas.AddPonto(MarcaId);
                Marca.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.arrowcircle));
                NumMarcas++;
            }
            
        }
        private void SalvarRota_Click(object sender, EventArgs e)
        {
            Rotas.InsertRota(_IdRota, _DescricaoRota);
            DesmarcaTodos();
        }

        private void DesmarcaTodos()
        {
            int nI = 0;

            for(nI=0;nI < MarcasRota.Count; nI++)
            {
                MarcasRota[nI].SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.arrowdown));
                Rotas.RemovePonto(MarcasRota[nI].Title.Split('-')[0]);
                NumMarcas--;
            }
            MarcasRota.RemoveRange(nI, MarcasRota.Count);

        }
    }























    [Activity(Label = "InfoRotas", MainLauncher = false)]
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
                
                TelaDoMapa.PutExtra("IdRota", ValorDIdDarota.Text);
                TelaDoMapa.PutExtra("Descricao", ValorDescricao.Text);
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