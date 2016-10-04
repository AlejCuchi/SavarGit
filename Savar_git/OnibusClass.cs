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

namespace Savar_git
{
    class OnibusClass // Collunm : numero_onibus:int | placa:varchar | X:double | y:double | z:double | rota:varchar | 
    {
        dbMngmt Database = new dbMngmt();
        public bool InputOnibus(int nNunOnibus, string cPlaca, double PosX, double PosY, double PosZ, string cRota )
        {
            bool lRet = false;
            string LogDatabase = "";
            dbMngmt Database = new dbMngmt();

            if(Database.ConectionTest(ref LogDatabase))
            {
                
            }
            return lRet;
        }
        public void SelectOnibus(int nNumero, string cPlaca, string cRota="")
        {
            string cQuery = "";
            string cLog = "";
            cQuery += "SELECT * FROM onibus ";
            cQuery += "WHERE numero_onibus = " + nNumero.ToString();
            cQuery += "  AND placa = "+ cPlaca ;
            if (Database.ConectionTest(ref cLog))
            {
                
            }
            //MySqlCommand Com = new MySqlCommand(Database,);
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