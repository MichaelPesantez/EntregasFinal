using EntregasFinal.WS;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace EntregasFinal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GrabarEntrega : ContentPage
    {
        private string codigoPersona;
        private double lati;
        private double lon;
        public GrabarEntrega (string nombresCompletos, string codigo, string nombre, string descripcion, string direccion, string persona, string categoria)
        {
            InitializeComponent ();
            ObtenerCategoria(categoria);
            localizar();
            lblNombre.Text = nombresCompletos;
            txtCodigo.Text = codigo;
            txtNombre.Text = nombre;
            txtDescripcion.Text = descripcion;
            txtDireccion.Text = direccion;
            codigoPersona = persona;
            mpMaps.IsVisible = true;
        }
        public async void ObtenerCategoria(string category)
        {
            string Url = "http://192.168.27.101/entregas/postcategory.php?codigo="+category;
            HttpClient cliente = new HttpClient();
            var content = await cliente.GetStringAsync(Url);
            var posts = JsonConvert.DeserializeObject<WS.Categoria>(content);
            txtCategoria.Text = posts.nombre.ToString();
        }

        private async void localizar()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            if(locator.IsGeolocationAvailable)
            {
                if(!locator.IsListening)
                {
                    await locator.StartListeningAsync(TimeSpan.FromSeconds(25), 5);
                }
                locator.PositionChanged += (cambio, args) =>
                {
                    var loc = args.Position;
                    txtLongitud.Text = loc.Longitude.ToString();
                    lon = loc.Longitude;
                    txtLatitud.Text = loc.Latitude.ToString();
                    lati = loc.Latitude;

                };
            }
            
        }
        private async void TomarFoto()
        { 
            var camara = new StoreCameraMediaOptions();
            camara.PhotoSize = PhotoSize.Medium;
            var foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);
            ImageSource imageSource = ImageSource.FromStream(foto.GetStream);
            imgFoto.Source = imageSource;
        }
        private void imgEntrega_Clicked(object sender, EventArgs e)
        {
            TomarFoto();
        }

        private void btnCargarMapa_Clicked(object sender, EventArgs e)
        {
            txtLatitud.IsVisible = true;
            txtLongitud.IsVisible = true;
            Pin MiUbi = new Pin()
            {
                Type= PinType.Place,
                Label="Mi Ubicacion",
                Position = new Position(lati, lon),
                Tag = "id_miubi"
            };
            mpMaps.Pins.Add(MiUbi);
            mpMaps.MoveToRegion(MapSpan.FromCenterAndRadius(MiUbi.Position, Distance.FromMeters(100)));
            mpMaps.MyLocationEnabled = true;
        }

        private void mpMaps_SelectedPinChanged(object sender, SelectedPinChangedEventArgs e)
        {
            
        }
    }
}