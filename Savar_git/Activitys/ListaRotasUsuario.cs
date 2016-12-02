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

namespace Savar_git
{
    [Activity(Label = "ListaRotasUsuario", MainLauncher = false)]
    public class ListaRotasUsuario : Activity
    {
        private LinearLayout LayoutMissing;
        private TextView Txto1, Txto2, Txto3;
        private ListView DataGridList;
        private EditText ID_ROta, DescricaoRota, QtdPontos;
        private List<RotaClass> RotasOnibus = new List<RotaClass>();
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
            
            Txto1.Text = "Id Rota";
            Txto2.Text = "Descrição";
            Txto3.Text = "Qtd Pontos";
            DataGridList.ItemClick += DataGridList_ItemClick;
            BuscaRotas();
            // Create your application here
        }

        private void DataGridList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent NewScreem = new Intent(this, typeof(MapaUser));
            NewScreem.PutExtra("Rota", RotasOnibus[e.Position].ID_ROta);
            NewScreem.PutExtra("Onibus", RotasOnibus[e.Position].NumeroOnibus);
            StartActivity(NewScreem);
        }
        void BuscaRotas()
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

            RotaClass rotaClass = new RotaClass();
            RespOnibus = rotaClass.SelectRota("",true);
            DataList = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            RotasOnibus.Clear();
            for (int i = 0; i < RespOnibus.Rows.Count; i++)
            {
                if (RotasOnibus.Count > 0)
                {
                    if (RotasOnibus[ContNum - 1].ID_ROta == RespOnibus.Rows[i]["id_rota"].ToString())
                    {
                        RotasOnibus[ContNum - 1].QtdPontos++;
                    }
                    else
                    {
                        ContNum = i;
                    }
                }
                else
                {
                    RotaClass Onibus = new RotaClass()
                    {
                        ID_ROta = RespOnibus.Rows[i]["id_rota"].ToString(),
                        DescricaoRota = RespOnibus.Rows[i]["desc_rota"].ToString(),
                        QtdPontos = 1,
                        NumeroOnibus = RespOnibus.Rows[i]["numero_onibus"].ToString()
                    };
                    ContNum = i;
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