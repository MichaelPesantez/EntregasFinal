using Newtonsoft.Json;
using System;
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
    public partial class ConsultarSupervisor : ContentPage
    {
        public ConsultarSupervisor(string nombresCompletos)
        {
            InitializeComponent();
            CargarRepartidor();
            lblNombre.Text = nombresCompletos;
        }

        private async void btnConsultar_Clicked(object sender, EventArgs e)
        {
            string Url = "http://192.168.27.104/entregas/postproducto.php?persona="+((WS.Personas)pcrRepartidor.SelectedItem).codigo;
            HttpClient cliente = new HttpClient();
            ObservableCollection<WS.Producto> post;
            var content = await cliente.GetStringAsync(Url);
            List<WS.Producto> posts = JsonConvert.DeserializeObject<List<WS.Producto>>(content);
            post = new ObservableCollection<WS.Producto>(posts);
            lista.ItemsSource = post;
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

        private void btnLimpiar_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {

        }
    }
}