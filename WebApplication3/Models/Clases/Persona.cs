using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Clases
{
    public class Persona
    {
        public Persona()
        {
            Ingresos = new List<Ingreso>();
        }
        public int id { get; set; }
        public string nombre { get; set; }
        public string tipo_persona {get;set;}
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public List<Ingreso> Ingresos { get; set; }
        public List<Venta> ventas { get; set; }
    }
}