using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class IngresoMap:EntityTypeConfiguration<Ingreso>
    {

        public IngresoMap()
        {
            ToTable("Ingreso");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("idingreso");
            HasRequired(a => a.Usuario).WithMany(a => a.Ingresos).HasForeignKey(a => a.idusuario);
            HasRequired(a => a.Proveedor).WithMany(a => a.Ingresos).HasForeignKey(a => a.idproveedor);
            HasMany(a => a.detalle_Ingresos).WithRequired(a => a.Ingreso).HasForeignKey(a => a.idingreso); 

        }
    }
}