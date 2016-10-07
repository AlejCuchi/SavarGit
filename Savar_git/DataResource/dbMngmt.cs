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
        public MySqlConnection Database = new MySqlConnection("Server=mysql.cogdzkecvymm.us-west-2.rds.amazonaws.com;Port=3306;database=Savar;User Id=admin;Password=felipe39;charset=utf8");
        private TextView SysLogVIewer;
        private Button NewButton;
        //private EditText TesteWIndow;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout1);
            SysLogVIewer = FindViewById<TextView>(Resource.Id.SysLog);
            //TesteWIndow = FindViewById<EditText>(Resource.Id.editText1);
            NewButton = FindViewById<Button>(Resource.Id.TesteButton);

            NewButton.Click += NewButton_Click;
            //TesteWIndow.Text = "Teste Botão1";

        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            SysLogVIewer.Text = "Teste base de dados.";
            if (ConectionTest())
            {
                SelectTable("onibus", "   ");
            }
            
        }
/**********************************************
    Testa conexão com o banco de dados.

    Retorna True, em caso da conexão estiver ativa. Caso a conexão estiver fechada, ele conecta com o banco.

***********************************************/
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
                    lRet = true;
                }
                SysLogVIewer.Text = "Conexão realizada com sucesso!!!";
            }
            catch (MySqlException ex)
            {
                SysLogVIewer.Text = ex.ToString();
            }
            
            return lRet;
        }


        public DataTable SelectTable(string Table, string cPrimaryKey = "")
        {
            DataTable TabRet = new DataTable();
            DataTable TableWork = new DataTable();
            MySqlDataAdapter MyData = new MySqlDataAdapter();
            MySqlCommand MyCommand =null;
            string cQuery = "";

            cQuery += "USE Savar; SELECT * FROM Savar." + Table + "  " ;
            if (cPrimaryKey == "")
            {
                cQuery += " ; ";
            }
            else
            {
                cQuery += "WHERE (SELECT"
            }

            MyCommand = new MySqlCommand(cQuery, Database);

            try
            {
                if (ConectionTest())
                {
                    MyData.SelectCommand = MyCommand;
                    MyData.Fill(TableWork);
                    SysLogVIewer.Text = "Tabela Preenchida";
                }
                
            }
            catch (MySqlException ex)
            {
                SysLogVIewer.Text = ex.ToString();
            }
            
            return TabRet;
        }
    }
}