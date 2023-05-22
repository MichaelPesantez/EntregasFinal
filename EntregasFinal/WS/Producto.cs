using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EntregasFinal.WS
{
    public class Producto
    {
        public int codigo { set; get; }
        public string nombre { set; get; }
        public string descripcion { set; get;}
        public string direccion { set; get; }
        public string Longitud { set; get;}
        public string Latitud { set; get;}
        public string fecha_entrega { set; get; }
        public string imagen { set; get; }
        public int persona { set; get; }
        public int categoria { set; get; }
        public int estado { set; get; }

    }
}
