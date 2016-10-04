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
using System.IO;

namespace Savar_git
{
    [Activity(Label = "SavarMap", MainLauncher = true , Icon = "@drawable/icon")]
    public class dbMngmt :Activity
    {
       
        MySqlConnection Database = new MySqlConnection("Server=mysql.cogdzkecvymm.us-west-2.rds.amazonaws.com;Port=3306;database=Savar;User Id=admin;Password=felipe39;charset=utf8");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            EditText TesteWIndow = FindViewById<EditText>(Resource.Id.editText1);
        }
        public bool ConectionTest(ref string Log  )
        {
            bool lRet = false;
            
            try
            {
                if (Database.State == ConnectionState.Closed)
                {
                    Database.Open();
                    lRet = true;
                }
                else
                {
                    Database.Close();
                    lRet = true;
                }

                
            }
            catch (MySqlException ex)
            {
                Log = ex.ToString();
            }
            return lRet;
        }
        public DataTable SelectTable(string Table, string cPrimaryKey)
        {
            DataTable TabRet = new DataTable();
            DataTable TableWork = new DataTable();
            MySqlDataAdapter MyData = new MySqlDataAdapter();
            string VarLog = "";
            string cQUery = "";

            cQUery += "SELECT KEYS FROM " + Table + " WHERE KEY_NAME = 'PRIMARY'; ";

            //MySqlCommand MyCommand = new MySqlCommand(Database, );
            if (ConectionTest(ref VarLog))
            {
                //MyData.SelectCommand = MyCommand;
                MyData.Fill(TableWork);

            }




            return TabRet;
        }
    }
}