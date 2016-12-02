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
using System.Data;

namespace Savar_git
{
    [Activity(Label = "Alterar_Usuario")]
    public class Alterar_Usuario : Activity
    {
        private UsuarioClass UserMngm  = new UsuarioClass();
        //private TextView TxV_DescTela;
        private EditText EdT_Nome;
        private EditText EdT_Usuario;
        private EditText EdT_Email;
        private EditText EdT_Senha;
        private EditText EdT_ConfirmaSenha;
        private Button Btn_Salvar;
        private CheckBox Chb_Func;
        private Button Btn_Buscar;
        private string cUsuario;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string Call = Intent.GetStringExtra("Tela") ?? "";
            this.cUsuario = Intent.GetStringExtra("User") ?? "";
            SetContentView(Resource.Layout.Usuario_Crud);
            
            EdT_Nome          = FindViewById<EditText>(Resource.Id.Campo_Usuario);
            EdT_Usuario       = FindViewById<EditText>(Resource.Id.EdT_UserSys);
            EdT_Email         = FindViewById<EditText>(Resource.Id.Campo_Email);
            EdT_Senha         = FindViewById<EditText>(Resource.Id.PrimeiraSenha);
            EdT_ConfirmaSenha = FindViewById<EditText>(Resource.Id.SegundaSenha);
            Btn_Salvar        = FindViewById<Button  >(Resource.Id.Btn_Salvar);
            Btn_Buscar        = FindViewById<Button  >(Resource.Id.Btn_BuscaUser);
            Chb_Func          = FindViewById<CheckBox>(Resource.Id.Chb_Funcionario);

            Chb_Func.Visibility   = Call == "AdmCall" ? ViewStates.Visible : ViewStates.Gone;
            Btn_Buscar.Visibility = Call == "AdmCall" ? ViewStates.Visible : ViewStates.Gone;
            EdT_Usuario.Enabled   = (Call != "Main_Screen_User");

            Btn_Salvar.Click += Btn_Salvar_Click;
            Btn_Buscar.Click += Btn_Buscar_Click;
            if(cUsuario != "")
            {
                FillFields();
            }
        }

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            System.Data.DataRow InfoUsuario;
            InfoUsuario = UserMngm.GetUser(EdT_Usuario.Text);
            if (InfoUsuario != null)
            {
                EdT_Nome.Text = InfoUsuario["nome"].ToString();
                EdT_Usuario.Text = InfoUsuario["email"].ToString();
                if (InfoUsuario["Tipo_conta"].ToString() == "1")
                {
                    Chb_Func.Checked = false;
                }
                else
                {
                    Chb_Func.Checked = true;
                }
            }
        }

        private void FillFields()
        {
            DataRow Usuario;
            Usuario          = UserMngm.GetUser(this.cUsuario);
            EdT_Nome.Text    = Usuario["Nome"].ToString();
            EdT_Usuario.Text = Usuario["Usuario"].ToString();
            EdT_Email.Text   = Usuario["Email"].ToString();
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
            if(cUsuario == "")
            {
                lValida = (cLog = ValidaSenha(Senha1, Senha2)) == "";
            }
            else if(EdT_Senha.Text != "")
            {
                lValida = (cLog = ValidaSenha(Senha1, Senha2)) == "";
            }
            if(lValida && NomeCompleto == "")
            {
                cLog = "Por favor, informe seu nome completo";
                lValida = false;
            }
            if (cUsuario == "" && lValida && UserMng.VerificaUsuario(this,Usuario,"").Length == 1)
            {
                cLog = "Usuário Existente!";
                lValida = false;
            }
            if(lValida && !EmailUsuario.Contains("@") && !EmailUsuario.Contains(".com"))
            {
                cLog = "Formato de email incorreto";
                lValida = false;
            }
            if (lValida && Usuario.Length < 6)
            {
                cLog = "Nome de usuário é muito curto!";
                lValida = false;
            }
            if(lValida && (cLog = cUsuario == "" ? UserMng.InsertUser(Usuario, Senha1, NomeCompleto,EmailUsuario,this): 
                                                   UserMng.UpdateUser(this,Usuario,"",Senha1,EmailUsuario,NomeCompleto)) != "")
            {
                cLog = "Problemas na inclusão do usuário : " + cLog;
            }
            else if (lValida && cLog.Length <1)
            {
                cLog = "Usuário Criado com sucesso!!!"; 
            }
            Toast.MakeText(this, cLog, ToastLength.Long).Show();
        }

        private string ValidaSenha(string Senha1, string Senha2)
        {
            string cLog = "";
            if (Senha1 != Senha2)
            {
                cLog = "As senhas não são iguais";
            }
            else if (Senha1.Length < 8)
            {
                cLog = "A senha não contem os requisitos minimos de segurança";
                
            }
            return cLog;
        }
    }
}