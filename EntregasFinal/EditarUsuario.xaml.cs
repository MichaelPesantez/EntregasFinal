using EntregasFinal.WS;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EntregasFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarUsuario : ContentPage
    {
        public EditarUsuario(string nombreCompleto)
        {
            InitializeComponent();
            lblNombre.Text = nombreCompleto;
            Obtener();
        }

        public async void Obtener()
        {
            string Url = "http://192.168.27.101/entregas/post.php";
            HttpClient cliente = new HttpClient();
            ObservableCollection<WS.Personas> post;
            var content = await cliente.GetStringAsync(Url);
            List<WS.Personas> posts = JsonConvert.DeserializeObject<List<WS.Personas>>(content);
            post = new ObservableCollection<WS.Personas>(posts);
            lista.ItemsSource = post;
        }
        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string codigo = (e.SelectedItem as Personas).codigo.ToString();
            string nombre = (e.SelectedItem as Personas).nombre.ToString();
            string apellido = (e.SelectedItem as Personas).apellido.ToString();
            string edad = (e.SelectedItem as Personas).edad.ToString();
            string cedula = (e.SelectedItem as Personas).cedula.ToString();
            string usuario = (e.SelectedItem as Personas).usuario.ToString();
            string contrasena = (e.SelectedItem as Personas).contrasena.ToString();
            string tipo = (e.SelectedItem as Personas).tipo.ToString();
            Navigation.PushAsync(new EliminarUsuario(lblNombre.Text, codigo, nombre, apellido, edad, cedula, usuario, contrasena, tipo));
        }
    }
}