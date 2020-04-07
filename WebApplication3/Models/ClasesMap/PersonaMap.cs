using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;

namespace WebApplication3.Models.ClasesMap
{
    public class PersonaMap:EntityTypeConfiguration<Persona>
    {
        public PersonaMap()
        {
            ToTable("Persona");
            HasKey(a => a.id);
            Property(a => a.id).HasColumnName("idpersona");



        }
    }
}