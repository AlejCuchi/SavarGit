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
using MySql.Data.MySqlClient;
using System.Data;

namespace Savar_git
{
    public class UsuarioClass
    {
        //private UsuarioClass UserLogado;
        private string cUserLogado;
        private string cLog;
        /* Fun��o VerificaUsuario
         * 
         * Retorno Boleano
         * Parametros
         *      AtualContext - Contexto Atual para a execu��o do Toast
         *      User = Usu�rio a ser verificado. Tipo String Obrigat�rio
         *      senha = Senha do usu�rio. Tipo String Opcional
         *      
         *      Caso n�o for passado a senha, ser� verificado apenas se o usu�rio existe.
         */
        

        public string VerUserLogado()
        {
            return this.cUserLogado;
        }
        public bool Logou(Context AtualContext, string User, string Senha)
        {
            if(VerificaUsuario(AtualContext,User,Senha).Length == 1)
            {
                this.cUserLogado = User;
                return true;
            }
            return false;
        }
        public string VerificaUsuario(Context AtualContext, string User, string senha ="" )
        {
            string cRet = "";
            DataRow UsuarioReg;
            UsuarioReg = GetUser(User);
            if(UsuarioReg != null)
            {
                cRet = UsuarioReg["Tipo_conta"].ToString();
            }
            else 
            {
                Toast.MakeText(AtualContext, this.cLog, ToastLength.Long);
            }
            return cRet;
        }
        public DataRow GetUser(string Usuario)
        {
            string cQuery = "";
            dbMngmt Database = new dbMngmt();
            MySqlDataAdapter MyData = new MySqlDataAdapter();
            DataTable Users = new DataTable();
            DataRow RowRet = null;

            this.cLog = "";
            cQuery += "USE Savar; ";
            cQuery += " Select  * from Savar.cliente";
            cQuery += " where  usuario = '" + Usuario.ToUpper() + "' ";
            try
            {
                if (Database.ConectionTest())
                {
                    MyData.SelectCommand = new MySqlCommand(cQuery, Database.Database);
                    MyData.Fill(Users);
                }
            }
            catch (MySqlException ex)
            {
                this.cLog = ex.ToString();
            }
            if (Users.Rows.Count > 0)
            {
                RowRet = Users.Rows[0];
            }
            return RowRet;
        }
        /*Insert User - Inser��o de usu�rios
         *  Fun��o realiza a cria��o de um novo usu�rio. 
         *  Retorno String - Caso de erro, retorna o log do erro.
         *  Parametros
         *      User - Nome do usu�rio Para ser incluso
         *      Senha - Senha do usu�rio novo.
         *      NomeUser - Nome completo do novo usu�rio
         *      email - Email do usu�rio
         *      AtualContext - Contexto para execu��o do Toast
         */
        public string InsertUser(string User, string Senha, string NomeUser,string email,Context AtualContext)
        {
            string cLog = "";
            dbMngmt Database = new dbMngmt();
            MySqlCommand Command;
            string cQuery = "";
            cQuery += "Use Savar; ";
            cQuery += "INSERT INTO Savar.cliente (usuario,senha,nome,email,Tipo_conta)  VALUES";
            cQuery += "( '"+User.ToUpper()+"' ,";
            cQuery += " '"+Senha+"' ,";
            cQuery += " '"+NomeUser+"' ,'"+email+"','1' ) ;";
            Command = new MySqlCommand(cQuery, Database.Database);
            try
            {
                if (Database.ConectionTest())
                {
                    if (VerificaUsuario(AtualContext, User.ToUpper()).Length == 1)
                    {
                        cLog = "Usu�rio j� existe! ";
                    }
                    else
                    {
                        Command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                cLog += ex.ToString();
            }
            return cLog;
        }
        /*UpdateUser - Atualiza��o de algum dado do usu�rio
         * Atualiza senha, nome e pontu���o do usu�rio.
         * Retorno String, retorna algum erro caso exista
         * Parametros
         *      User - Nome do usu�rio a ser alterado
         *      AtualContext - Conexto da tela, para execu��o do Toast
         *      User - Nome do usu�rio para encontrar no banco de dados
         *      Pontos - Nova quantidade de pontos do usu�rio
         *      Senha - Nova Senha do usu�rio
         *      Email - Novo Email do usu�rio
         *      Nome - Novo Nome do usu�rio
         *      Tipo conta - Altera o tipo da conta do usu�rio.
         */
        public string UpdateUser(Context AtualContext, string User, string Pontos = "", string Senha="" , string Email="",string Nome="",string TipoConta="")
        {
            string cLog = "";
            string cQuery = "";
            dbMngmt Database = new dbMngmt();
            MySqlCommand Command;
            cQuery += "USE Savar; ";
            cQuery += "UPDATE cliente";
            cQuery += " SET ";
            if (Senha != "" )
            {
                cQuery += " senha = '"+ Senha +" ";
                if((Pontos+Email+Nome+TipoConta).Length > 0)
                {
                    cQuery += " , ";
                }
            }
            if(Nome != "")
            {
                cQuery += " nome = '" + Nome + "' ";
                if ((Pontos + Email + TipoConta).Length > 0)
                {
                    cQuery += " , ";
                }
            }
            if(Pontos != "")
            {
                cQuery += " pontos = " + Pontos + " ";
                if ((Email + TipoConta).Length > 0)
                {
                    cQuery += " , ";
                }
            }
            if(TipoConta != "")
            {
                cQuery += " Tipo_conta = '" + TipoConta + " ";
                if (Email != "")
                {
                    cQuery += " , ";
                }
            }
            if(Email != "")
            {
                cQuery += " email = '" + Email + "' ";
            }
            cQuery += "Where usuario = '" + User.TrimEnd().TrimStart().ToUpper() + "' ;";
            Command = new MySqlCommand(cQuery, Database.Database);
            try
            {
                if (Database.ConectionTest())
                {
                    if (VerificaUsuario(AtualContext, User).Length == 1)
                    {
                        Command.ExecuteNonQuery();
                    }
                    else
                    {
                        cLog = "N�o foi poss�vel encontrar o usu�rio " + User.ToUpper() + "!";
                    }
                }
                else
                {
                    cLog = "Erro de conex�o com o banco de dados";
                }
            }
            catch (MySqlException ex)
            {
                cLog = ex.ToString();
            }
            return cLog;
        }
    }
}