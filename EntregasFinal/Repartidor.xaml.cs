using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EntregasFinal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Repartidor : ContentPage
	{
        private int idRepartidor;
		public Repartidor (string nombresCompletos,int codigo)
		{
			InitializeComponent ();
			lblNombre.Text = nombresCompletos;
            idRepartidor = codigo;
            
		}

        private void btnEntregar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Entregar(lblNombre.Text, idRepartidor));
        }

        private void btnConsultar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ConsultarRepartidor(lblNombre.Text, idRepartidor));
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Loigin());
        }
    }
}