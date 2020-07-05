using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class ArticuloMap: EntityTypeConfiguration<Articulo>
    {
        public ArticuloMap()
        {
            ToTable("Articulo");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("idarticulo");
            HasRequired(a => a.categoria).WithMany(a => a.Articulos).HasForeignKey(a => a.idcategoria);
           
        }
    }
}