using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class RolMap:EntityTypeConfiguration<Rol>
    {
        public RolMap()
        {
            ToTable("Rol");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("idrol");
        }
    }
}