using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class VentaMap : EntityTypeConfiguration<Venta>
    {
        public VentaMap()
        {
            ToTable("Venta");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("idventa");
            HasRequired(a => a.Usuario).WithMany(a => a.ventas).HasForeignKey(a => a.idusuario);
            HasRequired(a => a.Proveedor).WithMany(a => a.ventas).HasForeignKey(a => a.idcliente);
            HasMany(a => a.detalle_venta).WithRequired(a => a.Venta).HasForeignKey(a => a.idventa);

        }
    }
}