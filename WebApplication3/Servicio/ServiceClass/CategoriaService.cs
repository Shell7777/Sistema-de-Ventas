using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio.ServiceInterface;

namespace WebApplication3.Servicio.ServiceClass
{
    public class CategoriaService : IServiceCategoria
    {
       private  Context context = Context.GetContext();
        public List<Categoria> CatsList() {
            return context.Categorias.ToList();
        }
        public void CatAdd(Categoria categoria) {
            if (categoria != null) {
                context.Categorias.Add(categoria);
            }
        }
        public Categoria CatFind(int? id) {
            if (id==null)  return new Categoria();

            return context.Categorias.Find(id); 
        }
        public List<Categoria> CatsListEqualsName(string query) {
            
            return String.IsNullOrEmpty(query) ? new List<Categoria>():context.Categorias.Where(a => a.nombre.Contains(query)).ToList();
        }
        public void CatDrop(int? id) {
            if (id != null || id > 0)
                context.Categorias.Remove(context.Categorias.Find(id));
            
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public bool SiesUnico(string nombre )
        {
            return context.Categorias.Any(a => a.nombre == nombre);
        }
    }
}