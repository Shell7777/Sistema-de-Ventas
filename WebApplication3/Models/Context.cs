using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication3.Models.Clases;
using WebApplication3.Models.ClasesMap;

namespace WebApplication3.Models
{
    public class Context:DbContext 
    {
        public IDbSet<Articulo> Articulos { get; set; }
        public IDbSet<Categoria> Categorias{ get; set; }
        public IDbSet<Persona> Personas{ get; set; }
        public IDbSet<Rol> Rol { get; set; }
        public IDbSet<Venta> ventas{ get; set; }
        public IDbSet<Detalle_Venta> Detalle_Ventas{ get; set; }
        public IDbSet<Ingreso> Ingresos{ get; set; }
        public IDbSet<Detalle_Ingreso> Detalle_Ingresos{ get; set; }
        public IDbSet<Usuario> Usuarios{ get; set; }

        private static Context context;
        private Context()
        {
            Database.SetInitializer<Context>(null);
        }
        public static Context GetContext() {
            if (context == null) {
                context = new Context();
            }
            return context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ArticuloMap());
            modelBuilder.Configurations.Add(new CategoriaMap());
            modelBuilder.Configurations.Add(new PersonaMap());
            modelBuilder.Configurations.Add(new RolMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new VentaMap());
            modelBuilder.Configurations.Add(new Detalle_VentaMap());
            modelBuilder.Configurations.Add(new IngresoMap());
            modelBuilder.Configurations.Add(new Detalle_IngresoMap());
        }
    }
}