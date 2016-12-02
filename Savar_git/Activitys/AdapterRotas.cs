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
    public class AdapterRotas : BaseAdapter
    {
        private Activity activity;
        List<RotaClass> Onibus;

        public AdapterRotas(Activity activity, List<RotaClass> Onibus)
        {
            this.activity = activity;
            this.Onibus = Onibus;
        }

        public override int Count
        {
            get
            {
                return Onibus.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;

        }

        public override long GetItemId(int position)
        {
            return Onibus[position].QtdPontos;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var View = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.List_view_data_template, parent, false);
            var Text_PlacaOnibus = View.FindViewById<TextView>(Resource.Id.Placa_Onibus);
            var Num_NumeroOnibus = View.FindViewById<TextView>(Resource.Id.Numero_Onibus);
            var Stt_Onibus = View.FindViewById<TextView>(Resource.Id.BusStatus);
            Text_PlacaOnibus.Text = Onibus[position].ID_ROta.ToString();
            Num_NumeroOnibus.Text = Onibus[position].DescricaoRota.ToString();
            Stt_Onibus.Text = Onibus[position].QtdPontos.ToString();
            return View;
        }
    }
    
}