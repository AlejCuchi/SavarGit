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
        public void SelectOnibus(int nNumero, string cPlaca, string cRota="")
        {
            
            /*if (Database.ConectionTest())
            {
                
            }8*/
            
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