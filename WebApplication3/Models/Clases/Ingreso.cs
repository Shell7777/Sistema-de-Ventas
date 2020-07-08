using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Clases
{
    public class Ingreso
    {
        public Ingreso()
        {
            detalle_Ingresos = new List<Detalle_Ingreso>();
        }
        public int id { get; set; }
        public int idproveedor { get; set; }
        public int idusuario { get; set; }
        public string tipo_comprobante { get; set; }
        public string serie_comprobante { get; set; }
        public string num_comprobante { get; set; }
        public DateTime fecha_hora { get; set; }
        public decimal impuesto { get; set; }
        public decimal total { get; set; }
        public string estado { get; set; }


        public List<Detalle_Ingreso> detalle_Ingresos {get;set;}
        public Persona Proveedor { get; set; }
        public Usuario Usuario { get; set; }

    }
}