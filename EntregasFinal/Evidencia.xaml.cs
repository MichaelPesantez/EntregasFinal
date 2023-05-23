using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EntregasFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Evidencia : ContentPage
    {
        private int repartidor;
        public Evidencia(string nombresCompletos, string imagen, int codigo)
        {
            InitializeComponent();
            repartidor=codigo;
            lblNombre.Text = nombresCompletos;
            byte[] Base64Stream = Convert.FromBase64String(imagen);
            imgEvidencia.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Repartidor(lblNombre.Text, repartidor));
        }
    }
}