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
    public partial class EliminarCategoria : ContentPage
    {
        public EliminarCategoria(string nombresCompletos, string codigo, string nombre)
        {
            InitializeComponent();
            lblNombre.Text = nombresCompletos;
            txtCodigo.Text = codigo;
            txtNombre.Text = nombre;
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (await DisplayAlert("Confirmacion", "Esta Seguro de Editar los Datos", "SI", "NO"))
                {
                    WebClient cliente = new WebClient();
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    cliente.UploadValues("http://192.168.27.104/entregas/postcategory.php?codigo="+txtCodigo.Text+"&nombre="+txtNombre.Text, "PUT", parametros);
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
                    cliente.UploadValues("http://192.168.27.104/entregas/postcategory.php?codigo="+txtCodigo.Text, "DELETE", parametros);
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
            Navigation.PushAsync(new Administrador(lblNombre.Text));
        }
    }
}