using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Clases
{
    public class Usuario
    {
        public int id { get; set; }
        public int? idrol { get; set; }
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion{ get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public string password_salt { get; set; }
        public bool condicion { get; set; }

        public Rol roll { get; set; }
        public List<Ingreso> Ingresos { get; set; }
        public List<Venta> ventas{ get; set; }

    }
}