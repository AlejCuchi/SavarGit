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
        /* Função VerificaUsuario
         * 
         * Retorno Boleano
         * Parametros
         *      AtualContext - Contexto Atual para a execução do Toast
         *      User = Usuário a ser verificado. Tipo String Obrigatório
         *      senha = Senha do usuário. Tipo String Opcional
         *      
         *      Caso não for passado a senha, será verificado apenas se o usuário existe.
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
        /*Insert User - Inserção de usuários
         *  Função realiza a criação de um novo usuário. 
         *  Retorno String - Caso de erro, retorna o log do erro.
         *  Parametros
         *      User - Nome do usuário Para ser incluso
         *      Senha - Senha do usuário novo.
         *      NomeUser - Nome completo do novo usuário
         *      email - Email do usuário
         *      AtualContext - Contexto para execução do Toast
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
                        cLog = "Usuário já existe! ";
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
        /*UpdateUser - Atualização de algum dado do usuário
         * Atualiza senha, nome e pontuãção do usuário.
         * Retorno String, retorna algum erro caso exista
         * Parametros
         *      User - Nome do usuário a ser alterado
         *      AtualContext - Conexto da tela, para execução do Toast
         *      User - Nome do usuário para encontrar no banco de dados
         *      Pontos - Nova quantidade de pontos do usuário
         *      Senha - Nova Senha do usuário
         *      Email - Novo Email do usuário
         *      Nome - Novo Nome do usuário
         *      Tipo conta - Altera o tipo da conta do usuário.
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
                        cLog = "Não foi possível encontrar o usuário " + User.ToUpper() + "!";
                    }
                }
                else
                {
                    cLog = "Erro de conexão com o banco de dados";
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