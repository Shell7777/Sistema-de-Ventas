using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class CategoriaMap: EntityTypeConfiguration<Categoria>
    {
        public CategoriaMap()
        {
            ToTable("Categoria");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("idcategoria");
            
            /*HasMany(a => a.Articulos)
                .WithRequired(a => a.categoria)
                .HasForeignKey(a => a.idcategoria);*/
        }
    }
}