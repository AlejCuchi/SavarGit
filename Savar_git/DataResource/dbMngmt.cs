using System;

using Android.App;
using Android.OS;
using Android.Widget;
using MySql.Data.MySqlClient;
using System.Data;
using Savar_git;
using Android.Content;

namespace Savar_git
{
    [Activity(Label = "SavarMap", MainLauncher = false )]
    public class dbMngmt :Activity
    {
        public static string ConectString = "Server=mysql.cogdzkecvymm.us-west-2.rds.amazonaws.com;Port=3306;database=Savar;User Id=admin;Password=felipe39;charset=utf8";
        public MySqlConnection Database = new MySqlConnection(ConectString);
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
            OnibusClass obj = new OnibusClass(ConectString);
            if (ConectionTest())
            {
                SysLogVIewer.Text =  obj.UpdateOnibus("111", "aaa-3333",111,123,444);
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
                //SysLogVIewer.Text = "Conexão realizada com sucesso!!!";
            }
            catch (MySqlException ex)
            {
                ex.ToString();
                // SysLogVIewer.Text = ex.ToString();
            }
            
            return lRet;
        }


        public DataTable SelectTable(string Table, string cPrimaryKey = "")
        {
            DataTable TabRet = new DataTable();
            DataTable TableWork = new DataTable();
            MySqlDataAdapter MyData = new MySqlDataAdapter();
            MySqlCommand MyCommand =null;
            //OnibusClass ObjOnibus = new OnibusClass();
            string cQuery = "";

            cQuery += "USE Savar; SELECT * FROM Savar." + Table + "  " ;
            if (cPrimaryKey == "")
            {
                cQuery += " ; ";
            }
            else
            {
                cQuery += "WHERE (SELECT";
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