using EntregasFinal.WS;
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
    public partial class EliminarUsuario : ContentPage
    {
        public EliminarUsuario(string nombreCompleto, string codigo, string nombre, string apellido, string edad, string cedula, string usuario, string contrasena, string tipo)
        {
            InitializeComponent();
            int tipod = int.Parse(tipo);
            CargarPicker(tipod);
            pcrTipo.SelectedIndex = 2;
            lblNombre.Text = nombreCompleto;
            txtCodigo.Text = codigo;
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtEdad.Text = edad;
            txtCedula.Text = cedula;
            txtUsuario.Text = usuario;
            txtContrasena.Text = contrasena;
            

        }

        public async void CargarPicker(int numero)
        {
            string Url = "http://192.168.27.101/entregas/postuser.php";
            HttpClient cliente = new HttpClient();
            ObservableCollection<WS.Tipo> post;
            var content = await cliente.GetStringAsync(Url);
            List<WS.Tipo> posts = JsonConvert.DeserializeObject<List<WS.Tipo>>(content);
            pcrTipo.ItemsSource = posts;
            pcrTipo.ItemDisplayBinding = new Binding("nombre");
            pcrTipo.SelectedIndex = numero-1;

        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert("Confirmacion", "Esta Seguro de Editar los Datos", "SI", "NO"))
                {
                    WebClient cliente = new WebClient();
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    cliente.UploadValues("http://192.168.27.101/entregas/post.php?codigo="+txtCodigo.Text+"&nombre="+txtNombre.Text+
                        "&apellido="+txtApellido.Text+"&edad="+txtEdad.Text+"&cedula="+txtCedula.Text+"&usuario="+txtUsuario.Text+
                        "&contrasena="+txtContrasena.Text+"&tipo="+((WS.Tipo)pcrTipo.SelectedItem).codigo.ToString(), "PUT", parametros);
                    await DisplayAlert("Confirmacion", "Datos Actualizados", "Cerrar");
                    await Navigation.PushAsync(new Administrador(lblNombre.Text));
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert("Confirmacion", "Esta Seguro de Eliminar los Datos", "SI", "NO"))
                {
                    WebClient cliente = new WebClient();
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    cliente.UploadValues("http://192.168.27.101/entregas/post.php?codigo="+txtCodigo.Text, "DELETE", parametros);
                    await DisplayAlert("Confirmacion", "Datos Actualizados", "Cerrar");
                    await Navigation.PushAsync(new Administrador(lblNombre.Text));
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Loigin());
        }
    }
}