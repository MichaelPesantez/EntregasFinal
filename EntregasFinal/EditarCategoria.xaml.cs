using EntregasFinal.WS;
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
    public partial class EditarCategoria : ContentPage
    {
        public EditarCategoria(string nombresCompletos)
        {
            InitializeComponent();
            Obtener();
            lblNombre.Text = nombresCompletos;
        }

       public async void Obtener()
        {
            string Url = "http://192.168.27.101/entregas/postcategory.php";
            HttpClient cliente = new HttpClient();
            ObservableCollection<WS.Categoria> post;
            var content = await cliente.GetStringAsync(Url);
            List<WS.Categoria> posts = JsonConvert.DeserializeObject<List<WS.Categoria>>(content);
            post = new ObservableCollection<WS.Categoria>(posts);
            lista.ItemsSource = post;
        }
        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string codigo = (e.SelectedItem as Categoria).codigo.ToString();
            string nombre = (e.SelectedItem as Categoria).nombre.ToString();
            Navigation.PushAsync(new EliminarCategoria(lblNombre.Text, codigo, nombre));
        }
    }
}