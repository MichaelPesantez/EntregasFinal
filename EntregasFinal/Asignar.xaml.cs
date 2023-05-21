using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EntregasFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Asignar : ContentPage
    {
        public Asignar(string nombresCompletos)
        {
            InitializeComponent();
            CargarCategoria();
            CargarRepartidor();
            lblNombre.Text = nombresCompletos;
        }

        public async void CargarCategoria()
        {
            string Url = "http://192.168.27.104/entregas/postcategory.php";
            HttpClient cliente = new HttpClient();
            var content = await cliente.GetStringAsync(Url);
            List<WS.Categoria> posts = JsonConvert.DeserializeObject<List<WS.Categoria>>(content);
            pcrCategoria.ItemsSource = posts;
            pcrCategoria.ItemDisplayBinding = new Binding("nombre");
        }

        public async void CargarRepartidor()
        {
            string Url = "http://192.168.27.104/entregas/post.php?tipo=2";
            HttpClient cliente = new HttpClient();
            var content = await cliente.GetStringAsync(Url);
            List<WS.Personas> posts = JsonConvert.DeserializeObject<List<WS.Personas>>(content);
            pcrRepartidor.ItemsSource = posts;
            pcrRepartidor.ItemDisplayBinding = new Binding("usuario");
        }

        private void btnAsignar_Clicked(object sender, EventArgs e)
        {
            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("descripcion", txtDescripcion.Text);
                parametros.Add("persona", ((WS.Personas)pcrRepartidor.SelectedItem).codigo.ToString());
                parametros.Add("categoria", ((WS.Categoria)pcrCategoria.SelectedItem).codigo.ToString());
                parametros.Add("estado", "2");
                parametros.Add("Longitud", null);
                parametros.Add("Latitud", null);
                parametros.Add("fecha_entrega", null);
                parametros.Add("imagen", null);
                cliente.UploadValues("http://192.168.27.104/entregas/postproducto.php", "POST", parametros);
                DisplayAlert("ALERTA", "DATO INGRESADO", "Cerrar");
                txtNombre.Text = "";
                txtDescripcion.Text ="";
                pcrCategoria.SelectedIndex = 0;
                pcrRepartidor.SelectedIndex = 0;

            }
            catch (Exception ex)
            {

                DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Supervisor(lblNombre.Text));
        }
    }
}