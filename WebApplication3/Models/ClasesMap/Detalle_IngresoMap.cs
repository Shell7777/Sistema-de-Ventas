using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class Detalle_IngresoMap : EntityTypeConfiguration<Detalle_Ingreso>
    {
        public Detalle_IngresoMap()
        {
            ToTable("Detalle_ingreso");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("iddetalle_ingreso");
        }
    }
}