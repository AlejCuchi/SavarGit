using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Widget;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Savar_git;

namespace Savar_git
{
    [Activity(Label = "Main_Screen_User", MainLauncher = false)]
    public class Main_Screen_User : Activity
    {
        protected Button BtnHorario;
        protected Button BtnVerMapas;
        protected Button BtnJogos;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main_usuario);
            BtnHorario = FindViewById<Button>(Resource.Id.Btn_VerHorarios);
            BtnVerMapas = FindViewById<Button>(Resource.Id.Btn_VerMapas);
            BtnJogos = FindViewById<Button>(Resource.Id.Btn_Jogos);

            BtnHorario.Click += BtnHorario_Click;
            BtnVerMapas.Click += BtnVerMapas_Click;
        }

        private void BtnVerMapas_Click(object sender, EventArgs e)
        {
            Intent VerMapas = new Intent(this, typeof(MainActivity));
            StartActivity(VerMapas);
        }

        private void BtnHorario_Click(object sender, EventArgs e)
        {
            //Intent VerHorarios = new Intent(this,)
        }
    }
}