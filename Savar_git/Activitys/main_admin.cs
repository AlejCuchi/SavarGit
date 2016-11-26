using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Savar_git
{
    [Activity(Label = "main_admin")]
    public class main_admin : Activity
    {
        Button Btn_Onibus;
        Button Btn_Funcionarios;
        Button Btn_Pontos;
        Button Btn_RotasOnibus;
        Intent NextActivity;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_admin);
            Btn_Onibus = FindViewById<Button>(Resource.Id.Btn_AdmOnibus);
            Btn_Funcionarios = FindViewById<Button>(Resource.Id.Btn_AdmFunc);
            Btn_Pontos = FindViewById<Button>(Resource.Id.Btn_AdmPontos);
            Btn_RotasOnibus = FindViewById<Button>(Resource.Id.Btn_RtsPrds);
            Btn_Onibus.Click += Btn_Onibus_Click;
            Btn_Funcionarios.Click += Btn_Funcionarios_Click;
            Btn_Pontos.Click += Btn_Pontuacao_Click;
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
            NextActivity = new Intent(this, typeof(MapScreen));
            NextActivity.PutExtra("Tela","CadastroPontos");
            StartActivity(NextActivity);
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