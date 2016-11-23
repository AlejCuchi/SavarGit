using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Savar_git
{
    public class OnibusClass // Collunm : numero_onibus:int | placa:varchar | X:double | y:double | z:double | rota:varchar | 
    {
        
        public string Onibus_Placa { get; set; }
        public int Onibus_Numero { get; set; }
        public string Onibus_Status { get; set; }


        public bool ExisteOnibus(int nNumeroBus, string cPlaca)
        {
            DataTable OnibusTeste;
            bool lRet = false;
            OnibusTeste =  SelectOnibus( nNumeroBus ,cPlaca  );

            lRet = OnibusTeste.Rows.Count > 0;
            return lRet;
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

            Command = new MySqlCommand(cQuery, Database.GetDataBase());

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
            cQuery += "SELECT * FROM Savar.onibus ";
            if(nNumero != 0 || cPlaca != "" || cRota != "")
            {
                cQuery += " WHERE ";
            }
            if(nNumero != 0 )
            {
                cQuery += " numero_onibus = " + nNumero.ToString();
                if(cPlaca != "" || cRota != "")
                {
                    cQuery += " AND ";
                }
            }
            if(cPlaca != "" )
            {
                cQuery += " placa = '" +cPlaca + "' "  ;
                if (cRota != "")
                {
                    cQuery += " AND ";

                }
            }
            if(cRota != "")
            {
                cQuery += " rota = '" +cRota+ "' ";
            }

            cQuery += "; ";

            Command = new MySqlCommand(cQuery, Database.GetDataBase());
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
            string cRet = "";
            string cQuery = "";
            dbMngmt Database = new dbMngmt();
            MySqlCommand Command;

            cQuery += "USE Savar; ";
            cQuery += "UPDATE Savar.onibus ";
            
            cQuery += " SET  ";
            if (cNewNumOnibus != "")
            {
                cQuery += " numero_onibus = '" + cNewNumOnibus + "'  ";
                if(cNewPlaca != "" || PosX != 0)
                {
                    cQuery += " , ";
                }
            }
            if(cNewPlaca != "")
            {
                cQuery += " placa = '" + cNewPlaca + "'  ";
                if(PosX != 0)
                {
                    cQuery += " , ";
                }
            }
            if(PosX != 0 || PosY != 0 || PosZ != 0)
            {
                cQuery += " x = "+PosX.ToString().Replace(',','.')+" , y = "+PosY.ToString().Replace(',', '.') + " ,z =  "+PosZ.ToString().Replace(',', '.') + " ";
            }
            cQuery += "  WHERE numero_onibus = '"+ cNumOnibus +"' AND placa = '"+ cPlaca +"' ; ";
            Command = new MySqlCommand(cQuery, Database.GetDataBase());
            try
            {
                if (Database.ConectionTest())
                {
                    Command.ExecuteNonQuery();
                }
            }catch(MySqlException ex)
            {
               cRet =  ex.ToString();
            }
            return cRet;
        }
        public bool DeleteOnibus()
        {
            bool lRet=false;
            return lRet;
        }
    }
}