using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Locations;
using System.Threading.Tasks;
namespace Savar_git
{
    [Activity(Label = "Lista_onibus")]
    public class Lista_onibus : Activity
        {
        private EditText NumOnbius;
        private EditText PlacaOnibus;
        private EditText RotasOnibus;
        private ListView DataGridOnibus;
        private Button BuscaOnibus;
        private Location _currentLocation;
        private LocationManager _locationManager;
        private string _ProviderGps;
        private string _PosX, _PosY;
        private string cLog;
        private List<OnibusClass> ListOnibus = new List<OnibusClass>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.lista_onibus);
            DataGridOnibus = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            BuscaOnibus = FindViewById<Button>(Resource.Id.Btn_BuscarOnibus);
            BuscaOnibus.Click += BuscaOnibus_Click;
            DataGridOnibus.ItemClick += DataGridOnibus_ItemClick;
            InitializeLocationManager();
        }

        private void DataGridOnibus_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            OnibusClass OnibusSelecionado = ListOnibus[e.Position];
            string cPlaca = OnibusSelecionado.Onibus_Placa;
            string cNumero = OnibusSelecionado.Onibus_Numero.ToString();

            
        }
        

        private void BuscaOnibus_Click(object sender, EventArgs e)
        {
            ListView DataList = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            
            int NumeroOnibus = 0;
            string cPlaca = "";
            string cRota = "";
            NumOnbius = FindViewById<EditText>(Resource.Id.Txt_OnibusNumero);
            PlacaOnibus = FindViewById<EditText>(Resource.Id.Txt_PlacaOnibus);
            RotasOnibus = FindViewById<EditText>(Resource.Id.Txt_Rotas);
            System.Data.DataTable RespOnibus;

            NumeroOnibus = NumOnbius.Text == "" ? 0 : Convert.ToInt32(NumOnbius.Text);
            cPlaca = PlacaOnibus.Text;
            cRota = RotasOnibus.Text;

            OnibusClass onibusClass = new OnibusClass();
            RespOnibus = onibusClass.SelectOnibus(NumeroOnibus, cPlaca, cRota);
            DataList = FindViewById<ListView>(Resource.Id.LiV_Onibus);
            ListOnibus.Clear();
            for (int i = 0; i < RespOnibus.Rows.Count; i++)
            {
                OnibusClass Onibus = new OnibusClass()
                {
                    Onibus_Numero = Convert.ToInt32(RespOnibus.Rows[i]["numero_onibus"]),
                    Onibus_Placa = RespOnibus.Rows[i]["placa"].ToString()
                };
                ListOnibus.Add(Onibus);
            }
            if (RespOnibus.Rows.Count > 0)
            {
                
                var adapter = new CustomAdapter(this, ListOnibus);
                DataList.Adapter =  adapter;
                

            }

        }
        private void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria CriteriosGPS = new Criteria { Accuracy = Accuracy.Fine };

            IList<string> ProvidersDisponiveis = _locationManager.GetProviders(CriteriosGPS, true);

            if (ProvidersDisponiveis.Any())
            {
                _ProviderGps = ProvidersDisponiveis.First();
            }
            else
            {
                _ProviderGps = string.Empty;
            }
        }
        
        
        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            Geocoder geocoder = new Geocoder(this);
            IList<Address> addressList =
                await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

            Address address = addressList.FirstOrDefault();
            return address;
        }
        private string DisplayAddress(Address address)
        {
            string cRet = "";
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                cRet = deviceAddress.ToString();
            }
            else
            {
                cLog += "\n\n Unable to determine the address. Try again in a few minutes.";
            }

            return cRet;
        }

        public async void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                cLog += "\n\n Unable to determine your location. Try again in a short while.";
            }
            else
            {
                _PosX = _currentLocation.Latitude.ToString();
                _PosY = _currentLocation.Longitude.ToString();
                Address address = await ReverseGeocodeCurrentLocation();
                
            }
        }
    }
}