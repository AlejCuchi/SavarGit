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

namespace Savar_git
{
    [Activity(Label = "RotasMapas")]
    public class RotasMapas : Activity 
    {
        private LinearLayout LayoutMissing;
        private TextView Txto1, Txto2, Txto3;
        Button BuscaOnibus;
        private ListView DataGridList;
        private EditText ID_ROta, DescricaoRota, QtdPontos;
        private List<RotaClass> RotasOnibus = new List<RotaClass>();
        private string NumOnibus, PlacaOnibus;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.lista_onibus);
            LayoutMissing = FindViewById<LinearLayout>(Resource.Id.LayoutListaOnibus);
            LayoutMissing.Visibility = ViewStates.Gone;
            Txto1 = FindViewById<TextView>(Resource.Id.textView7);
            Txto2 = FindViewById<TextView>(Resource.Id.textView8);
            Txto3 = FindViewById<TextView>(Resource.Id.textView9);
            DataGridList = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            BuscaOnibus = FindViewById<Button>(Resource.Id.Btn_BuscarOnibus);
            NumOnibus = Intent.GetStringExtra("NumeOnibus") ?? "";
            PlacaOnibus = Intent.GetStringExtra("PlacaOnibus") ?? "";
            Txto1.Text = "Id Rota";
            Txto2.Text = "Descri��o";
            Txto3.Text = "Qtd Pontos";
            BuscaOnibus.Click += BuscaOnibus_Click;
            DataGridList.ItemClick += DataGridList_ItemClick;
            
            // Create your application here
        }

        private void DataGridList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            List<string> Valores = new List<string>();
            Valores.Add( NumOnibus);
            Valores.Add(PlacaOnibus);
            Valores.Add(RotasOnibus[e.Position].ID_ROta);
            Valores.Add(RotasOnibus[e.Position].DescricaoRota);
            Intent MapsScreen = new Intent(this, typeof(MapScreen));
            MapsScreen.PutStringArrayListExtra("Array", Valores);
            StartActivity(MapsScreen);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();

        }
        private void BuscaOnibus_Click(object sender, EventArgs e)
        {
            ListView DataList = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            int ContNum = 0;
            string cPlaca = "";
            string cRota = "";
            ID_ROta = FindViewById<EditText>(Resource.Id.Txt_OnibusNumero);
            DescricaoRota = FindViewById<EditText>(Resource.Id.Txt_PlacaOnibus);
            QtdPontos = FindViewById<EditText>(Resource.Id.Txt_Rotas);
            System.Data.DataTable RespOnibus;
            
            cPlaca = DescricaoRota.Text;
            cRota = QtdPontos.Text;

            RotaClass onibusClass = new RotaClass();
            RespOnibus = onibusClass.SelectRota(ID_ROta.Text);
            DataList = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            RotasOnibus.Clear();
            for (int i = 0; i < RespOnibus.Rows.Count; i++)
            {
                if (RotasOnibus.Count > 0)
                {
                    if (RotasOnibus[ContNum].ID_ROta == RespOnibus.Rows[i]["id_rota"].ToString())
                    {
                        RotasOnibus[ContNum].QtdPontos++;
                    }
                    else
                    {
                        ContNum = i;
                        RotaClass Onibus = new RotaClass()
                        {
                            ID_ROta = RespOnibus.Rows[i]["id_rota"].ToString(),
                            DescricaoRota = RespOnibus.Rows[i]["desc_rota"].ToString(),
                            QtdPontos = 1
                        };
                        RotasOnibus.Add(Onibus);
                    }
                }
                else
                {
                    ContNum = i;
                    RotaClass Onibus = new RotaClass()
                    {
                        ID_ROta = RespOnibus.Rows[i]["id_rota"].ToString(),
                        DescricaoRota = RespOnibus.Rows[i]["desc_rota"].ToString(),
                        QtdPontos = 1
                    };
                    RotasOnibus.Add(Onibus);
                }
            }
            if (RespOnibus.Rows.Count > 0)
            {

                var adapter = new AdapterRotas(this, RotasOnibus);
                DataList.Adapter = adapter;


            }

        }
    }
}