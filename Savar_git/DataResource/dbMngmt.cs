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
        string Log = "";
        MySqlConnection Database = new MySqlConnection("Server=mysql.ogdzkecvymm.us-west-2.rds.amazonaws.com;Port=3306;database=Savar;User Id=admin;Password=felipe39;charset=utf8");
        private TextView SysLogVIewer;
        private Button NewButton;
        private EditText TesteWIndow;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout1);
            SysLogVIewer = FindViewById<TextView>(Resource.Id.SysLog);
            TesteWIndow = FindViewById<EditText>(Resource.Id.editText1);
            NewButton = FindViewById<Button>(Resource.Id.TesteButton);

            NewButton.Click += NewButton_Click;
            TesteWIndow.Text = "Teste Botão1";

        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            SysLogVIewer.Text = "Teste base de dados.";
            try
            {
                if (Database.State == ConnectionState.Closed)
                {
                    Database.Open();
                    SysLogVIewer.Text = "Conectado com sucesso";
                }
                
            }
            catch (MySqlException ex)
            {
                SysLogVIewer.Text = ex.ToString();
            }
            
        }

        public bool ConectionTest()
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
            
            string cQUery = "";

            cQUery += "SELECT KEYS FROM " + Table + " WHERE KEY_NAME = 'PRIMARY'; ";

            MySqlCommand MyCommand = new MySqlCommand(cQUery, Database);
            if (ConectionTest())
            {
                MyData.SelectCommand = MyCommand;
                MyData.Fill(TableWork);
                Console.WriteLine(" ");
            }
            return TabRet;
        }
    }
}