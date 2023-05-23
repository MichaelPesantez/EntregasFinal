using EntregasFinal.WS;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        public string base64foto;
       
        public GrabarEntrega (string nombresCompletos, string codigo, string nombre, string descripcion, string direccion, string persona, string categoria)
        {
            InitializeComponent();
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
            camara.PhotoSize = PhotoSize.Small;
            camara.CompressionQuality = 10;
            camara.RotateImage = true;
            camara.SaveToAlbum = true;
            var foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);
            ImageSource imageSource = ImageSource.FromStream(foto.GetStream);
            imgFoto.Source = imageSource;
            Stream stream1 = foto.GetStream();
            base64foto = await Convertbase64Async(stream1);
            
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

        public static async Task<string> Convertbase64Async(Stream stream)
        {
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes, 0, (int)stream.Length);
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
        private void mpMaps_SelectedPinChanged(object sender, SelectedPinChangedEventArgs e)
        {
            
        }

        private async void btnGuardarEntrega_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert("Confirmacion", "Realizar la Entrega?", "SI", "NO"))
                {
                    WebClient cliente = new WebClient();
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    cliente.UploadValues("http://192.168.27.101/entregas/postproducto.php?codigo="+txtCodigo.Text+"&Longitud="+txtLongitud.Text+"&Latitud="+txtLatitud.Text+
                        "&fecha_entrega="+dtFecha.Date.ToString()+"&imagen="+base64foto+"&estado=1","PUT", parametros);
                    await DisplayAlert("Confirmacion", "Entrega Realizada", "Cerrar");
                    await Navigation.PushAsync(new Entregar(lblNombre.Text, Convert.ToInt32(codigoPersona)));
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }
    }
}