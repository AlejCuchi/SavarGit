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
        private string cUser;
        private Button BtnHorario;
        private Button BtnVerMapas;
        private Button BtnConfig;
        private TextView TeV_NomeUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            UsuarioClass UserMgnm = new UsuarioClass();

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main_usuario);
            BtnHorario = FindViewById<Button>(Resource.Id.Btn_VerHorarios);
            BtnVerMapas = FindViewById<Button>(Resource.Id.Btn_VerMapas);
            BtnConfig = FindViewById<Button>(Resource.Id.Btn_ConfgUser);
            TeV_NomeUser = FindViewById<TextView>(Resource.Id.TeV_UserName);

            cUser = Intent.GetStringExtra("Tela") ?? "";
            TeV_NomeUser.Text = UserMgnm.VerUserLogado();
            BtnHorario.Click += BtnHorario_Click;
            BtnVerMapas.Click += BtnVerMapas_Click;
            BtnConfig.Click += BtnConfig_Click;
        }

        private void BtnConfig_Click(object sender, EventArgs e)
        {
            Intent ActivityNew = new Intent(this, typeof(Alterar_Usuario));
            ActivityNew.PutExtra("Tela", "Main_Screen_User");
            ActivityNew.PutExtra("User", cUser);
            StartActivity(ActivityNew);
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