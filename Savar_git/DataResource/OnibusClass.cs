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
using MySql.Data.MySqlClient;

namespace Savar_git
{
    class OnibusClass // Collunm : numero_onibus:int | placa:varchar | X:double | y:double | z:double | rota:varchar | 
    {
        private string ConnectionString = "";

        public OnibusClass(string StringConect)
        {
            ConnectionString = StringConect;
        }
/**************************************
 * Função InputOnibus - Função para realizar inclusão de novo ônibus
 * Parâmetros 
 *      cNunOnibus - Variavel String com o número identificado  do Onibus 
 *      cPlaca - Placa do Veículo 
 *      cRota - Identificador de uma rota que o ônibus vai fazer.
 **************************************/
        public void InputOnibus(string  cNunOnibus , string cPlaca , string cRota="" )
        {
            string lRet = "";
            dbMngmt Database = new dbMngmt();
            string cQuery = "";
            MySqlDataAdapter OnibusData = new MySqlDataAdapter();
            MySqlCommand Command = null;


            cQuery += "USE Savar; ";
            cQuery += "INSERT INTO Savar.onibus (numero_onibus, placa ";
            if(cRota != "")
            {
                cQuery += ", rota) ";
            }
            else
            {
                cQuery += ") ";
            }
            cQuery += " VALUES (" +cNunOnibus+ ",  '"+ cPlaca+"' ";
            if (cRota != "")
            {
                cQuery += ", "+cRota+" ) ;";
            }
            else
            {
                cQuery += ") ;";
            }

            Command = new MySqlCommand(cQuery, Database.Database);

            try
            {
                if (Database.ConectionTest())
                {
                    Command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                lRet = ex.ToString();
            }
        }
/*********************************
 * Função SelectOnibus()
 * Parametros : 
 *      nNumero = Número do Onibus.
 *      cPlaca  = String com a placa do Veículo
 *      cRota   = Rota De ônibus
 *  Deve ser passado o parametro nNumero e cPlaca, ou Somemente o cRota, 
 *  caso quera verificar os ônibus de uma determinada rota.
 *  
 * ******************************/
        public DataTable SelectOnibus(int nNumero = 0, string cPlaca= "", string cRota="")
        {
            dbMngmt Database = new dbMngmt();
            DataTable Onibus = new DataTable();
            MySqlDataAdapter OnibusData = new MySqlDataAdapter();
            MySqlCommand Command = null;
            string cQuery = "";

            cQuery += "USE Savar; ";
            cQuery += "Select * from Savar.onibus ";
            cQuery += "WHERE ";
            
            if(cRota == "")
            {
                cQuery += "numero_onibus = " + nNumero.ToString() + " AND ";
                cQuery += "placa = " + cPlaca + " ;";
            }
            else
            {
                cQuery = " rota = " + cRota + " ;";
            }
            Command = new MySqlCommand(cQuery, Database.Database);
            try
            {
                if (Database.ConectionTest())
                {
                    OnibusData.SelectCommand = Command;
                    OnibusData.Fill(Onibus);

                }
            }
            catch (MySqlException ex)
            {
                ex.ToString();
            }
            return Onibus;
        }


        public string UpdateOnibus(string cNumOnibus, string cPlaca, double PosX=0, double PosY=0, double PosZ=0, string cNewNumOnibus="", string cNewPlaca="")
        {
            string lRet = "";
            string cQuery = "";
            dbMngmt Database = new dbMngmt();
            MySqlCommand Command;

            cQuery += "USE Savar; ";
            cQuery += "UPDATE Savar.onibus ";
            
            cQuery += " SET  ";
            if (cNewNumOnibus != "")
            {
                cQuery += " numero_onibus = '" + cNewNumOnibus + "' , ";
            }
            if(cNewPlaca != "")
            {
                cQuery += " placa = '" + cNewPlaca + "' , ";
            }
            if(PosX != 0 || PosY != 0 || PosZ != 0)
            {
                cQuery += " x = "+PosX.ToString()+" , y = "+PosY.ToString()+" ,z =  "+PosZ.ToString()+" ";
            }
            cQuery += "  WHERE numero_onibus = '"+ cNumOnibus +"' AND placa = '"+ cPlaca +"' ; ";
            Command = new MySqlCommand(cQuery, Database.Database);
            try
            {
                if (Database.ConectionTest())
                {
                    Command.ExecuteNonQuery();
                }
            }catch(MySqlException ex)
            {
                lRet = ex.ToString();
            }
            return lRet;
        }
        public bool DeleteOnibus()
        {
            bool lRet=false;
            return lRet;
        }
    }
}