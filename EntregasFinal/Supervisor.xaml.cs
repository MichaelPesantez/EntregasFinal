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
	public partial class Supervisor : ContentPage
	{
		public Supervisor (string nombresCompletos)
		{
			InitializeComponent ();
			lblNombre.Text = nombresCompletos;
		}

        private void btnAsignar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Asignar(lblNombre.Text));
        }

        private void btnConsultar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultarSupervisor(lblNombre.Text));
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Loigin());
        }
    }
}