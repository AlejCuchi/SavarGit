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

        OnibusClass(string StringConect)
        {
            ConnectionString = StringConect;
        }
        public bool InputOnibus(int nNunOnibus, string cPlaca, double PosX, double PosY, double PosZ, string cRota )
        {
            bool lRet = false;

            
            return lRet;
        }
        public void SelectOnibus(int nNumero = 0, string cPlaca= "", string cRota="")
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
                
            }
            
            
        }
        public bool UpdateOnibus()
        {
            bool lRet = false;
            return lRet;
        }
        public bool DeleteOnibus()
        {
            bool lRet=false;
            return lRet;
        }
    }
}