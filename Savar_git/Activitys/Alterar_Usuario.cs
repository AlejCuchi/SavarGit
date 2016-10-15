using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Savar_git;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Savar_git
{
    [Activity(Label = "Alterar_Usuario")]
    public class Alterar_Usuario : Activity
    {
        TextView TxV_DescTela;
        EditText EdT_Nome;
        EditText EdT_Usuario;
        EditText EdT_Email;
        EditText EdT_Senha;
        EditText EdT_ConfirmaSenha;
        Button Btn_Salvar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Usuario_Crud);
            
            TxV_DescTela = FindViewById<TextView>(Resource.Id.TextView_NomeTela);
            EdT_Nome = FindViewById<EditText>(Resource.Id.Campo_Usuario);
            EdT_Usuario = FindViewById<EditText>(Resource.Id.EdT_UserSys);
            EdT_Email = FindViewById<EditText>(Resource.Id.Campo_Email);
            EdT_Senha = FindViewById<EditText>(Resource.Id.PrimeiraSenha);
            EdT_ConfirmaSenha = FindViewById<EditText>(Resource.Id.SegundaSenha);
            Btn_Salvar = FindViewById<Button>(Resource.Id.Btn_Salvar);
            TxV_DescTela.Text = "Criando Usu�rios";

            Btn_Salvar.Click += Btn_Salvar_Click;
            
        }

        private void Btn_Salvar_Click(object sender, EventArgs e)
        {
            string NomeCompleto, Usuario, EmailUsuario, Senha1, Senha2, cLog = "";
            UsuarioClass UserMng = new UsuarioClass();
            bool lValida = true;
            NomeCompleto = EdT_Nome.Text;
            Usuario = EdT_Usuario.Text;
            EmailUsuario = EdT_Email.Text;
            Senha1 = EdT_Senha.Text;
            Senha2 = EdT_ConfirmaSenha.Text;
            
            if (Senha1 != Senha2 )
            {
                cLog = "As senhas n�o s�o iguais";
                lValida = false;    
            }
            else if(lValida && Senha1.Length < 8)
            {
                cLog = "A senha n�o contem os requisitos minimos de seguran�a";
                lValida = false;   
            }
            if(lValida && NomeCompleto == "")
            {
                cLog = "Por favor, informe seu nome completo";
                lValida = false;
                
            }
            if (lValida && UserMng.VerificaUsuario(Usuario))
            {
                cLog = "Usu�rio Existente!";
                lValida = false;
            }
            if(lValida && !EmailUsuario.Contains("@") && !EmailUsuario.Contains(".com"))
            {
                cLog = "Formato de email incorreto";
                lValida = false;

            }
            if (lValida && Usuario.Length < 6)
            {

                cLog = "Nome de usu�rio � muito curto!";
                lValida = false;
            }
            if(lValida && (cLog = UserMng.InsertUser(Usuario, Senha1, NomeCompleto,EmailUsuario)) != "")
            {
                cLog = "Problemas na inclus�o do usu�rio : " + cLog;
            }
            else if (lValida)
            {
                cLog = "Usu�rio Criado com sucesso!!!"; 
            }
            Toast.MakeText(this, cLog, ToastLength.Long).Show();
        }
    }
}