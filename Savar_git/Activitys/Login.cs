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
    [Activity(Label = "Login", MainLauncher = true) ]
    
    public class Login : Activity
    {
        Intent UserLogado;
        EditText EdT_Usuario;
        EditText EdT_Senha;
        Button Btn_CriaConta;
        Button Btn_Entrar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            Btn_CriaConta = FindViewById<Button>(Resource.Id.Btn_CriaConta);
            Btn_Entrar = FindViewById<Button>(Resource.Id.Btn_Entrar);
            EdT_Senha = FindViewById<EditText>(Resource.Id.EdT_Senha);
            EdT_Usuario =  FindViewById<EditText> (Resource.Id.EdT_Usuario);

            Btn_Entrar.Click += Btn_Entrar_Click;
            Btn_CriaConta.Click += Btn_CriaConta_Click;
        }

        private void Btn_Entrar_Click(object sender, EventArgs e)
        {
            UsuarioClass UserMngm = new UsuarioClass();
            string LocalUsuario, LocalSenha;
            
            LocalUsuario = EdT_Usuario.Text;
            LocalSenha = EdT_Senha.Text;

            if(LocalUsuario =="" || LocalSenha == "")
            {
                Toast.MakeText(this,"Usuário Inválido",ToastLength.Long).Show();
            }
            else
            {
                if(UserMngm.VerificaUsuario(LocalUsuario, LocalSenha))
                {
                    this.UserLogado = new Intent(this, typeof(Main_Screen_User));
                    StartActivity(this.UserLogado);
                }
                else
                {
                    Toast.MakeText(this, "Usuário ou senha Incorreto", ToastLength.Long).Show();
                }
            }
            
        }

        private void Btn_CriaConta_Click(object sender, EventArgs e)
        {
            this.UserLogado = new Intent(this, typeof(Alterar_Usuario));
            StartActivity(UserLogado);
        }
    }
}