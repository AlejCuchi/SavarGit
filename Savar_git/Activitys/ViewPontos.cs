using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Android.Gms.Maps;

namespace Savar_git
{
   
    class ViewPontos :  DialogFragment
    {
        private View view;
        private EditText Descricao;
        private EditText Latitude;
        private EditText Longitude;
        private Button ApagarPonto;
        private double nLatitude, nLongitude;
        private string cDescricao;
        private bool lSaiu = false;

        public bool HasAdd()
        {
            return lSaiu;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle saverInstanceState)
        {
            base.OnCreateView(inflater, container, saverInstanceState);
            
            view = inflater.Inflate(Resource.Layout.MenuManutencaoPonto, container, false);
            
            Descricao = view.FindViewById<EditText>(Resource.Id.PontoDescri);
            Latitude = view.FindViewById<EditText>(Resource.Id.PontoLatitude);
            Longitude = view.FindViewById<EditText>(Resource.Id.PontoLongitude);
            ApagarPonto = view.FindViewById<Button>(Resource.Id.PontoApagar);

            nLatitude  = Arguments.GetDouble("Latitude" );
            nLongitude = Arguments.GetDouble("Longitude");
            cDescricao = Arguments.GetString("Descricao");

            Latitude.Text = nLatitude.ToString();
            Longitude.Text = nLongitude.ToString();
            Descricao.Text = cDescricao;

            ApagarPonto.Click += ApagarPonto_Click;
            return view;
        }
        private void ApagarPonto_Click(object sender, EventArgs e)
        {
            PontosClass Ponto = new PontosClass();
            string IdPonto = cDescricao.Split('|')[0];
            string cRet = "";
            cRet = Ponto.DeletaPonto(IdPonto);
            lSaiu = true;
            this.Dismiss();
        }
    }
}