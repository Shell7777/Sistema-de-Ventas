using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Clases
{
    public class Rol
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
        public List<Usuario> Usuarios { get; set; }

    }
}