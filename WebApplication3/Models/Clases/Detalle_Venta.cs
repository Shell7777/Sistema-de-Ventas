using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Clases
{
    public class Detalle_Venta
    {
        public int id { get; set; }
        public int idventa { get; set; }
        public int idarticulo { get; set; }
        public int cantidad {get;set;}
        public decimal precio{ get; set; }
        public decimal descuento { get; set; }
        public Venta Venta { get; set; }
    }
}