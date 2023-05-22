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
	public partial class Entregar : ContentPage
	{
		private int id;
		public Entregar (string nombresCompletos, int idRepartidor)
		{
			InitializeComponent ();
			lblNombre.Text = nombresCompletos;
			id = idRepartidor;
            Obtener();
		}

		public async void Obtener()
		{
            string Url = "http://192.168.27.101/entregas/postproducto.php?persona="+id.ToString()+"&estado=2";
            HttpClient cliente = new HttpClient();
            ObservableCollection<WS.Producto> post;
            var content = await cliente.GetStringAsync(Url);
            List<WS.Producto> posts = JsonConvert.DeserializeObject<List<WS.Producto>>(content);
            post = new ObservableCollection<WS.Producto>(posts);
            lista.ItemsSource = post;
        }

        private void lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            string codigo = (e.SelectedItem as WS.Producto).codigo.ToString();
            string nombre = (e.SelectedItem as WS.Producto).nombre.ToString();
            string descripcion = (e.SelectedItem as WS.Producto).descripcion.ToString();
            string direccion = (e.SelectedItem as WS.Producto).direccion.ToString();
            string persona = (e.SelectedItem as WS.Producto).persona.ToString();
            string categoria = (e.SelectedItem as WS.Producto).categoria.ToString();
            Navigation.PushAsync(new GrabarEntrega(lblNombre.Text, codigo, nombre, descripcion, direccion, persona, categoria));
        }
    }
}