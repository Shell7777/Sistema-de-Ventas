using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class UsuarioMap: EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("idusuario");
            HasRequired(a => a.roll).WithMany(a => a.Usuarios).HasForeignKey(a => a.idrol);
        }
    }
}