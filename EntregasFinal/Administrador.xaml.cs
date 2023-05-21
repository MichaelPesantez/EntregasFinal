using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EntregasFinal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Administrador : ContentPage
	{
		public Administrador (string nombreCompletos)
		{
			InitializeComponent ();
			lblNombre.Text = nombreCompletos;
		}

        private void btnNuevoUsuario_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearUsuario(lblNombre.Text));
        }

        private void btnEditarUsuario_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditarUsuario(lblNombre.Text));
        }

        private void btnNuevaCategoria_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearCategoria(lblNombre.Text));
        }

        private void btnEditarCategoria_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditarCategoria(lblNombre.Text));
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Loigin());
        }
    }
}