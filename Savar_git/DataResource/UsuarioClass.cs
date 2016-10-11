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
        /* Função VerificaUsuario
         * 
         * Retorno Boleano
         * Parametros
         *      User = Usuário a ser verificado. Tipo String Obrigatório
         *      senha = Senha do usuário. Tipo String Opcional
         *      
         *      Caso não for passado a senha, será verificado apenas se o usuário existe.
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
        /*Insert User - Inserção de usuários
         *  Função realiza a criação de um novo usuário. 
         *  Retorno String - Caso de erro, retorna o log do erro.
         *  Parametros
         *      User - Nome do usuário Para ser incluso
         *      Senha - Senha do usuário novo.
         *      NomeUser - Nome completo do novo usuário
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
                cLog = ex.ToString();
            }
            return cLog;
        }

        /*UpdateUser - Atualização de algum dado do usuário
         * Atualiza senha, nome e pontuãção do usuário.
         * Retorno String, retorna algum erro caso exista
         * Parametros
         *      User - Nome do usuário a ser alterado
         *      Param - Parametro com o valor da açao a ser tomada
         *          1- Troca de senha
         *          2- Troca de Nome do usuário
         *          3- Atualização da pontuação do usuário
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
                        cLog = "Não foi possível encontrar o usuário " + User + "!";
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