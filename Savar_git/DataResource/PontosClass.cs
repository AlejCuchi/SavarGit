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
using Android.Gms.Maps
using Android.Gms.Maps.Model;

namespace Savar_git
{
    class PontosClass
    {
        public DataTable SelectPonto(string IdPonto = "",string DescricaoPonto ="")
        {
            DataTable Pontos = new DataTable();
            MySqlCommand Comando;
            MySqlDataAdapter PontosAdapter = new MySqlDataAdapter();
            dbMngmt Database = new dbMngmt();
            

            string cQuery = "";

            cQuery += "SELECT * FROM Savar.ponto_onibus ";

            if (IdPonto != "")
            {
                cQuery += "  ID_ponto = '"+IdPonto+"' ";
                if(DescricaoPonto !="")
                {
                    cQuery += " AND ";
                }
            }
            if(DescricaoPonto != "")
            {
                cQuery += "Descricao_ponto = '%" + DescricaoPonto + "%' ";
            }
            
            cQuery += ";";

            Comando = new MySqlCommand(cQuery, Database.GetDataBase());
            try
            {
                if (Database.ConectionTest())
                {
                    PontosAdapter.SelectCommand = Comando;
                    PontosAdapter.Fill(Pontos);
                }
                else
                {
                    Pontos = null;
                }
            }
            catch (MySqlException ex)
            {
                ex.ToString();
                Pontos = null;
            }
            return Pontos;
        }


        public bool InsertPonto( string Descricao_ponto, double PosiX, double PosiY)
        {
            string cQuery = "";
            MySqlCommand Comand;
            dbMngmt Database = new dbMngmt();
            DataTable MaxId = new DataTable();
            bool lValida = true;

            cQuery += "INSERT INTO Savar.ponto_onibus (Descricao_ponto,x,y) ";
            cQuery += "VALUES ('"+Descricao_ponto+"', "+PosiX.ToString()+"," +PosiY.ToString()+" ) ;";

            Comand = new MySqlCommand(cQuery, Database.GetDataBase());

            try
            {
                if (Database.ConectionTest())
                {
                    Comand.ExecuteNonQuery();
                }
                else
                {
                    lValida = false;
                }

            }
            catch (MySqlException ex)
            {
                ex.ToString();
                lValida = false;
            }
            return lValida;
        }
        public bool UpdatePonto(string IdPonto, string Descricao_ponto = "", double PosiX= 0, double PosiY = 0)
        {
            string cQuery = "";
            MySqlCommand Comand;
            dbMngmt Database = new dbMngmt();
            bool lValida = true;

            cQuery += "UPDATE Savar.ponto_onibus ";
            cQuery += " SET ";
            if (Descricao_ponto != "")
            {
                cQuery += "Descricao_ponto = '"+Descricao_ponto + "' ";
                if(PosiX != 0 || PosiY != 0)
                {
                    cQuery += " , ";
                }
                
            }
            if(PosiX != 0 || PosiY != 0)
            {
                cQuery += " x = " + PosiX.ToString() + " , y = " + PosiY.ToString() + " ";
            }
            cQuery += "WHERE ID_ponto = "+IdPonto+" ; ";

            Comand = new MySqlCommand(cQuery, Database.GetDataBase());

            try
            {
                if (Database.ConectionTest())
                {
                    Comand.ExecuteNonQuery();
                }
                else
                {
                    lValida = false;
                }
            }
            catch(MySqlException ex)
            {
                ex.ToString();
                lValida = false;
            }
            


            return lValida;
        }


        public GoogleMap CarregaPontosMapa(GoogleMap googleMap)
        {
            DataTable PontosProMapa = new DataTable();
            PontosProMapa = SelectPonto();
            foreach (DataRow Item in PontosProMapa.Rows)
            {
                
                googleMap.AddMarker(new MarkerOptions()
                       .SetPosition(new LatLng(Convert.ToDouble( Item["x"]), Convert.ToDouble( Item.["y"])))
                       .SetTitle(Item["ID_ponto"].ToString() +"|" + Item["Descricao_ponto"].ToString()));
            }
            return googleMap;
        }
    }
}