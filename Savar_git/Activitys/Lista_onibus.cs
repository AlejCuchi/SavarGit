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
    [Activity(Label = "Lista_onibus")]
    public class Lista_onibus : Activity
    {
        private EditText NumOnbius;
        private EditText PlacaOnibus;
        private GridView DataGridOnibus;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.lista_onibus);
            NumOnbius = FindViewById<EditText>(Resource.Id.Txt_OnibusNumero);
            PlacaOnibus = FindViewById< EditText > (Resource.Id.Txt_PlacaOnibus);
            DataGridOnibus = FindViewById<GridView>(Resource.Id.Grd_ListaOnibus);
            
            // Create your application here
        }
    }
}