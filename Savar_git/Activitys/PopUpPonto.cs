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
    class PopUpPonto : DialogFragment
    {
        protected View view;
        private bool AddPonto;
        public  override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle saverInstanceState)
        {
            base.OnCreateView(inflater, container, saverInstanceState);

            view = inflater.Inflate(Resource.Layout.PontoPopUp, container, false);
            Button Btn_SalvarPonto = view.FindViewById<Button>(Resource.Id.Btn_SalvarPonto);
            Btn_SalvarPonto.Click += Btn_SalvarPonto_Click;
            return view;

        }
        public bool HasAdd()
        {
            return AddPonto;
        }
        private void Btn_SalvarPonto_Click(object sender, EventArgs e)
        {
            EditText Descric = view.FindViewById<EditText>(Resource.Id.DescricaoPonto);
            PontosClass Ponto = new PontosClass();
            double Lat, Log;
            Lat = Arguments.GetDouble("Latitude");
            Log = Arguments.GetDouble("Longitude");
            if (Descric.Text == "")
            {
                Toast.MakeText(view.Context, "Descrição Invalida", ToastLength.Long);
            }
            else
            {
                if(Ponto.InsertPonto(Descric.Text, Lat, Log))
                {
                    this.Dismiss();
                    AddPonto = true;
                }
            }
        }


    }
}