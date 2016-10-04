using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using Savar_git;
using Android;

namespace Savar_git
{
    [Activity(Label = "SavarMap", MainLauncher = false, Icon = "@drawable/icon")]

    public class MainActivity : Activity, IOnMapReadyCallback
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.mapFrag);
            mapFragment.GetMapAsync(this);

        }
        public struct Position
        {
            public Double PosX;
            public Double PosY;
            public string Desc;
            public Position(Double PosiX, Double PosiY, string Descri)
            {
                PosX = PosiX;
                PosY = PosiY;
                Desc = Descri;
            }

        };

        public void OnMapReady(GoogleMap googleMap)
        {

            CriaPontos(googleMap);
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;

            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());
        }
        private void CriaPontos(GoogleMap googleMap)
        {


            List<Position> Locations = GetReferences();

            // Conecta banco
            // Busca Pontos -26.242570, -48.815741
            foreach (Position item in Locations)
            {
                googleMap.AddMarker(new MarkerOptions()
                       .SetPosition(new LatLng(item.PosX, item.PosY))
                       .SetTitle(item.Desc));
            }
        }
        private List<Position> GetReferences()
        {
            List<Position> Lista = new List<Position>();
            Position Local;
            Local = new Position(-26.242570, -48.915741, "Marca1");
            Lista.Add(Local);

            Local = new Position(-26.242570, -48.815541, "Marca 2");
            Lista.Add(Local);

            Local = new Position(-27.242570, -48.845741, "Marca 3");
            Lista.Add(Local);

            Local = new Position(-26.245570, -48.835741, "Marca 4");
            Lista.Add(Local);

            return Lista;
        }

    }
}

