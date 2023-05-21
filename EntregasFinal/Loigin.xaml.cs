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
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace EntregasFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loigin : ContentPage
    {
        private readonly HttpClient cliente = new HttpClient();
        public Loigin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Clicked(object sender, EventArgs e)
        {
            Validar();
        }

        public async void Validar()
        {
            try 
            {
                string Url = "http://192.168.27.104/entregas/post.php?usuario="+txtUsuario.Text+"&contrasena="+txtContrasena.Text;
                var content = await cliente.GetStringAsync(Url);
                var posts = JsonConvert.DeserializeObject<WS.Personas>(content);
                if (posts.tipo == 1) 
                {
                    string nombre = posts.nombre+" "+posts.apellido;
                    await Navigation.PushAsync(new Administrador(nombre));
                }
                if (posts.tipo == 2)
                {
                    string nombre = posts.nombre+" "+posts.apellido;
                    await Navigation.PushAsync(new Supervisor(nombre));
                }
                if (posts.tipo == 3)
                {
                    string nombre = posts.nombre+" "+posts.apellido;
                    await Navigation.PushAsync(new Repartidor(nombre));
                }
            }
            catch (Exception ex) 
            {
                await DisplayAlert("ALERTA", "Usuario/Contrasena incorrecto", "Cerrar");
                txtUsuario.Text = "";
                txtContrasena.Text =  "";
            }
            
        }
    }
}