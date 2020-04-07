using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Clases
{
    public class Detalle_Ingreso
    {
        public int id { get; set; }
        public int idingreso { get; set; }
        public int idarticulo { get; set; }
        public int cantidad {get;set;}
        public decimal precio { get; set; }

        public Ingreso Ingreso { get; set; }

    }
}