using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EntregasFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCategoria : ContentPage
    {
        public CrearCategoria(string nombresCompletos)
        {
            InitializeComponent();
            lblNombre.Text = nombresCompletos; 
        }

        private void btnGurdar_Clicked(object sender, EventArgs e)
        {

            try
            {
                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
                parametros.Add("nombre", txtNombre.Text);
                cliente.UploadValues("http://192.168.27.101/entregas/postcategory.php", "POST", parametros);
                DisplayAlert("ALERTA", "DATO INGRESADO", "Salir");
                txtNombre.Text = " ";

            }
            catch (Exception ex)
            {

                DisplayAlert("ALERTA", ex.Message, "Cerrar");
            }
        }

        private void btnRegresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Administrador(lblNombre.Text));
        }
    }
}