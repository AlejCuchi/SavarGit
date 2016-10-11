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
    class UsuarioClass
    {
        /* Fun��o VerificaUsuario
         * 
         * Retorno Boleano
         * Parametros
         *      User = Usu�rio a ser verificado. Tipo String Obrigat�rio
         *      senha = Senha do usu�rio. Tipo String Opcional
         *      
         *      Caso n�o for passado a senha, ser� verificado apenas se o usu�rio existe.
         */ 
        public bool VerificaUsuario(string User, string senha ="")
        {
            bool lRet = false;
            string cQuery = "";
            dbMngmt Database = new dbMngmt();
            MySqlCommand Command;
            MySqlDataAdapter MyData = new MySqlDataAdapter();
            DataTable Users = new DataTable();
            cQuery += "USE Savar; " ;
            cQuery += "Select  * from Savar.cliente";
            cQuery += "where usuario = '"+User+"' ";
            if(senha == "")
            {
                cQuery += "; ";
            }
            else
            {
                cQuery += "  AND    senha = '" + senha + "'; ";
            }
            

            Command = new MySqlCommand(cQuery, Database.Database);

            MyData.SelectCommand = Command;
            MyData.Fill(Users);
            if(Users.Rows[0].ToString() != "")
            {
                lRet = true;
            }

            return lRet;
        }
        /*Insert User - Inser��o de usu�rios
         *  Fun��o realiza a cria��o de um novo usu�rio. 
         *  Retorno String - Caso de erro, retorna o log do erro.
         *  Parametros
         *      User - Nome do usu�rio Para ser incluso
         *      Senha - Senha do usu�rio novo.
         *      NomeUser - Nome completo do novo usu�rio
         */
        public string InsertUser(string User, string Senha, string NomeUser)
        {
            string cLog = "";
            dbMngmt Database = new dbMngmt();
            MySqlCommand Command;
            string cQuery = "";

            cQuery += "Use Savar; ";
            cQuery += "Insert Into clientes  Values";
            cQuery += "( '"+User+" ,";
            cQuery += " '"+Senha+" ,";
            cQuery += " '"+NomeUser+", 0 ) ;";
            Command = new MySqlCommand(cQuery, Database.Database);
            try
            {
                if (Database.ConectionTest())
                {
                    if (VerificaUsuario(User))
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
                cLog = ex.ToString();
            }
            return cLog;
        }

        /*UpdateUser - Atualiza��o de algum dado do usu�rio
         * Atualiza senha, nome e pontu���o do usu�rio.
         * Retorno String, retorna algum erro caso exista
         * Parametros
         *      User - Nome do usu�rio a ser alterado
         *      Param - Parametro com o valor da a�ao a ser tomada
         *          1- Troca de senha
         *          2- Troca de Nome do usu�rio
         *          3- Atualiza��o da pontua��o do usu�rio
         *      ValorParam - Novo valor a ser substituido.
         *      
         */
        public string UpdateUser(string User, string Param , string ValorParam="")
        {
            string cLog = "";
            string cQuery = "";
            dbMngmt Database = new dbMngmt();
            MySqlCommand Command;

            cQuery += "USE Savar; ";
            cQuery += "UPDATE cliente";
            cQuery += "SET ";
            if (Param =="1" )
            {
                cQuery += "senha = '"+ValorParam+" ";
            }
            else if(Param == "2")
            {
                cQuery += " nome = '" + ValorParam + " ";
            }
            else if(Param == "3")
            {
                cQuery += " pontos = " + ValorParam + " ";
            }
            else
            {
                cLog = "Parametro de processo invalido;";
            }
            cQuery += "Where usuario = '" +User +"' ;";


            Command = new MySqlCommand(cQuery, Database.Database);

            try
            {
                if (Database.ConectionTest())
                {
                    if (VerificaUsuario(User))
                    {
                        Command.ExecuteNonQuery();
                    }
                    else
                    {
                        cLog = "N�o foi poss�vel encontrar o usu�rio " + User + "!";
                    }
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