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
    [Activity(Label = "main_funcionario")]
    public class main_funcionario : Activity
    {
        Button Btn_Onibus;
        Button Btn_Funcionarios;
        Button Btn_Pontuacao;
        Button Btn_RotasOnibus;
        Intent NextActivity;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_admin);
            Btn_Onibus = FindViewById<Button>(Resource.Id.Btn_AdmOnibus);
            Btn_Funcionarios = FindViewById<Button>(Resource.Id.Btn_AdmFunc);
            Btn_Pontuacao = FindViewById<Button>(Resource.Id.Btn_AdmPontos);
            Btn_RotasOnibus = FindViewById<Button>(Resource.Id.Btn_RtsPrds);
            
            Btn_Onibus.Click += Btn_Onibus_Click;
            Btn_Funcionarios.Click += Btn_Funcionarios_Click;
            Btn_Pontuacao.Click += Btn_Pontuacao_Click;
            Btn_RotasOnibus.Click += Btn_RotasOnibus_Click;
        }

        private void Btn_RotasOnibus_Click(object sender, EventArgs e)
        {

            NextActivity = new Intent(this, typeof(Horarios));
            NextActivity.PutExtra("Tela", "AdmCall");
            StartActivity(NextActivity);
        }

        private void Btn_Pontuacao_Click(object sender, EventArgs e)
        {
            //NextActivity = new Intent(this,typeof())
        }

        private void Btn_Funcionarios_Click(object sender, EventArgs e)
        {
            NextActivity = new Intent(this, typeof(Alterar_Usuario));
            NextActivity.PutExtra("Tela", "AdmCall");
            StartActivity(NextActivity);
        }

        private void Btn_Onibus_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(Lista_onibus)));
        }


    }
}