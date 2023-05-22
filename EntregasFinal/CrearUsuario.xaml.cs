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
    public partial class CrearUsuario : ContentPage
    {
        public CrearUsuario(string nombreCompleto)
        {
            InitializeComponent();
            lblNombre.Text = nombreCompleto;
            CargarPicker();
        }

        public async void CargarPicker()
        {
            string Url = "http://192.168.27.101/entregas/postuser.php";
            HttpClient cliente = new HttpClient();
            var content = await cliente.GetStringAsync(Url);
            List<WS.Tipo> posts = JsonConvert.DeserializeObject<List<WS.Tipo>>(content);
            pcrTipo.ItemsSource = posts;
            pcrTipo.ItemDisplayBinding = new Binding("nombre");
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {

            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("nombre", txtNombre.Text);
                parametros.Add("apellido", txtApellido.Text);
                parametros.Add("edad", txtEdad.Text);
                parametros.Add("cedula", txtCedula.Text);
                parametros.Add("usuario", txtUsuario.Text);
                parametros.Add("contrasena", txtContrasena.Text);
                parametros.Add("tipo", ((WS.Tipo)pcrTipo.SelectedItem).codigo.ToString());
                cliente.UploadValues("http://192.168.27.101/entregas/post.php", "POST", parametros);
                DisplayAlert("ALERTA", "DATO INGRESADO", "Cerrar");
                txtNombre.Text = "";
                txtApellido.Text ="";
                txtCedula.Text = "";
                txtEdad.Text = "";
                txtUsuario.Text = "";
                txtContrasena.Text = "";
                pcrTipo.SelectedItem = 0;

            }
            catch (Exception ex)
            {

                DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Administrador(lblNombre.Text));
        }
    }
}