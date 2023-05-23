using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EntregasFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultarRepartidor : ContentPage
    {
        
        public ConsultarRepartidor(string nombresCompletos, int idRepartidor)
        {
            InitializeComponent();
            lblNombre.Text = nombresCompletos;
            Obtener(idRepartidor);
            
 
        }
        public async void Obtener(int inter)
        {
            string Url = "http://192.168.27.101/entregas/postproducto.php?persona="+inter+"&estado=1";
            HttpClient cliente = new HttpClient();
            ObservableCollection<WS.Producto> post;
            var content = await cliente.GetStringAsync(Url);
            List<WS.Producto> posts = JsonConvert.DeserializeObject<List<WS.Producto>>(content);
            post = new ObservableCollection<WS.Producto>(posts);
            lista.ItemsSource = post;
        }

        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string imagen = (e.SelectedItem as WS.Producto).imagen.ToString();
            int codigo = (e.SelectedItem as WS.Producto).persona;
            Navigation.PushAsync(new Evidencia(lblNombre.Text,imagen,codigo));
        }
    }
}