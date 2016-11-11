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
using Java.Lang;

namespace Savar_git
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView NumeroOnibus { get; set; }
        public TextView PlacaOnibus { get; set; }
        public TextView StatusOnibus { get; set; }
    }
    public class CustomAdapter : BaseAdapter
    {
        private Activity activity;
        List<OnibusClass> Onibus;

        public CustomAdapter(Activity activity, List<OnibusClass> Onibus)
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
            return Onibus[position].Onibus_Numero;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var View = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.List_view_data_template, parent, false);
            var Text_PlacaOnibus = View.FindViewById<TextView>(Resource.Id.Placa_Onibus);
            var Num_NumeroOnibus = View.FindViewById<TextView>(Resource.Id.Numero_Onibus);
            var Stt_Onibus = View.FindViewById<TextView>(Resource.Id.BusStatus);
            Text_PlacaOnibus.Text = Onibus[position].Onibus_Placa;
            Num_NumeroOnibus.Text = Onibus[position].Onibus_Numero.ToString();
            Stt_Onibus.Text = Onibus[position].Onibus_Status;
            return View; 
        }
    }
}