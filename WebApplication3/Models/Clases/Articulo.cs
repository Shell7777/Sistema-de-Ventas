using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Clases
{
    public class Articulo
    {
        public int? id{ get; set; }
        public int? idcategoria { get; set; }
        public string codigo { get; set; }
        public string nombre{ get; set; }
        public decimal? precio_venta { get; set; }
        public  int? stock { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
        public Categoria categoria { get; set; }
    }
}