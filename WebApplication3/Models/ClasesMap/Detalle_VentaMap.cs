using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class Detalle_VentaMap : EntityTypeConfiguration<Detalle_Venta>
    {
        public Detalle_VentaMap()
        {
            ToTable("Detalle_venta");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("iddetalle_venta");
        }
    }
}