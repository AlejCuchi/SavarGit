using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Savar_git
{
    [Activity(Label = "Lista_onibus")]
    public class Lista_onibus : Activity
        {
        private EditText NumOnbius;
        private EditText PlacaOnibus;
        private EditText RotasOnibus;
        private ListView DataGridOnibus;
        private Button BuscaOnibus;
        
        private List<OnibusClass> ListOnibus = new List<OnibusClass>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.lista_onibus);
            DataGridOnibus = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            BuscaOnibus = FindViewById<Button>(Resource.Id.Btn_BuscarOnibus);
            BuscaOnibus.Click += BuscaOnibus_Click;
            DataGridOnibus.ItemClick += DataGridOnibus_ItemClick;
            
        }

        private void DataGridOnibus_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            OnibusClass OnibusSelecionado = ListOnibus[e.Position];
            
            string cPlaca = OnibusSelecionado.Onibus_Placa;
            string cNumero = OnibusSelecionado.Onibus_Numero.ToString();
            Android.Content.Intent MapsOnibus;
            
            if (OnibusSelecionado.ExisteOnibus(Convert.ToInt32(cNumero), cPlaca))
            {
                MapsOnibus = new Android.Content.Intent(this, typeof(MapScreen));
                MapsOnibus.PutExtra("NumeOnibus", cNumero);
                MapsOnibus.PutExtra("PlacaOnibus", cPlaca);
                StartActivity(MapsOnibus);
            }   
        }
        private void BuscaOnibus_Click(object sender, EventArgs e)
        {
            ListView DataList = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            int NumeroOnibus = 0;
            string cPlaca = "";
            string cRota = "";
            NumOnbius = FindViewById<EditText>(Resource.Id.Txt_OnibusNumero);
            PlacaOnibus = FindViewById<EditText>(Resource.Id.Txt_PlacaOnibus);
            RotasOnibus = FindViewById<EditText>(Resource.Id.Txt_Rotas);
            System.Data.DataTable RespOnibus;

            NumeroOnibus = NumOnbius.Text == "" ? 0 : Convert.ToInt32(NumOnbius.Text);
            cPlaca = PlacaOnibus.Text;
            cRota = RotasOnibus.Text;

            OnibusClass onibusClass = new OnibusClass();
            RespOnibus = onibusClass.SelectOnibus(NumeroOnibus, cPlaca, cRota);
            DataList = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            ListOnibus.Clear();
            for (int i = 0; i < RespOnibus.Rows.Count; i++)
            {
                OnibusClass Onibus = new OnibusClass()
                {
                    Onibus_Numero = Convert.ToInt32(RespOnibus.Rows[i]["numero_onibus"]),
                    Onibus_Placa = RespOnibus.Rows[i]["placa"].ToString()
                };
                ListOnibus.Add(Onibus);
            }
            if (RespOnibus.Rows.Count > 0)
            {
                
                var adapter = new CustomAdapter(this, ListOnibus);
                DataList.Adapter =  adapter;
                

            }

        }
        
        
        
        
    }
}