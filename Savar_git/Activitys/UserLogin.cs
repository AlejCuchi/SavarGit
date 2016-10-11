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


namespace Savar_git.Activitys
{
    [Activity(Label = "UserLogin", MainLauncher = true, Icon = "@drawable/icon")]

    public class UserLogin : Activity
    {
        private EditText EdTxtUsuario;
        private EditText EdtSenha;
        private Button BtnEntrar;
        private Button BtnCriarConta;
        private Intent OtherScreem;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            EdTxtUsuario = FindViewById<EditText>(Resource.Id.EdT_Usuario);
            EdtSenha = FindViewById<EditText>(Resource.Id.EdT_Senha);
            BtnEntrar = FindViewById<Button>(Resource.Id.Btn_Entrar);
            BtnCriarConta = FindViewById<Button>(Resource.Id.Btn_CriaConta);

            BtnEntrar.Click += BtnEntrar_Click;
            BtnCriarConta.Click += BtnCriarConta_Click;
        }

        private void BtnCriarConta_Click(object sender, EventArgs e)
        {
            //Voltar
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            string User, Senha;
            User = EdTxtUsuario.Text;
            Senha = EdtSenha.Text;
            UsuarioClass MngUser = new UsuarioClass();
            if (MngUser.VerificaUsuario(User, Senha))
            {
                OtherScreem = new Intent(this, typeof(Main_Screen_User));
                StartActivity(OtherScreem);
            }
            else
            {
                Toast.MakeText(this, "O usuário ou a senha está incorreta! ", ToastLength.Long);
            }
            
        }
    }
}