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
	public partial class Repartidor : ContentPage
	{
		public Repartidor (int id,string nombre,string apellido,string cedula,int edad,int tipo)
		{
			InitializeComponent ();
		}
	}
}