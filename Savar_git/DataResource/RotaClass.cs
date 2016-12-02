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
using Android.Gms.Maps.Model;
using System.Data;

namespace Savar_git
{     
    class RotaClass
    {
        
        private int NumPontos;
        private List<string> PontosRota;

        public bool ExistRota(string id_rota)
        {
            bool lValida = false;

            if(SelectRota(id_rota) != null)
            {
                lValida = true;
            }

            return lValida;
        }

        private DataTable SelectRota(string id_rota)
        {
            DataTable MyData = new DataTable();
            MySqlCommand Command;
            dbMngmt Database = new dbMngmt();
            MySqlDataAdapter MyRotas = new MySqlDataAdapter();
            string cQuery = "";

            cQuery += "SELECT * FROM Savar.Rota WHERE id_rota = '"+id_rota+"'";
            Command = new MySqlCommand(cQuery,Database.GetDataBase());
            try
            {
                if (Database.ConectionTest())
                {
                    MyRotas.SelectCommand = Command;
                    MyRotas.Fill(MyData);
                }
            }
            catch (MySqlException ex)
            {
                ex.ToString();
            }

            return MyData;
        }



        public void AddPonto(string Marca)
        {
            if(PontosRota == null)
            {
                PontosRota = new List<string> { Marca };
            }
            else
            {
                PontosRota.Add(Marca);
            }
            this.NumPontos++;
        }



        public void RemovePonto(string Marca)
        {
            PontosRota.Remove(Marca);
        }




        public bool InsertRota(string Idrota, string DescricaoRota)
        {
            bool lVerifica = true;
            string cQuery = "";
            MySqlCommand Command;
            dbMngmt Database = new dbMngmt();
            cQuery += "INSERT INTO Savar.rota (id_rota,desc_rota,codigo_sequencia,id_ponto) VALUES "  ;
            for (int nI = 0; nI < PontosRota.Count; nI++)
            {
                if (nI > 0)
                {
                    cQuery += " , ";
                }
                cQuery += "  ('" + Idrota + "','" + DescricaoRota + "','" 
                                 + nI.ToString() + "'," +PontosRota[nI] + ") ";
            }
            cQuery += ";";

            Command = new MySqlCommand(cQuery, Database.GetDataBase());
            try
            {
                if (Database.ConectionTest())
                {
                    Command.ExecuteNonQuery();
                }
            }
            catch(MySqlException ex)
            {
                ex.ToString();
                lVerifica = false;
            }
            return lVerifica;
        }
    }
}