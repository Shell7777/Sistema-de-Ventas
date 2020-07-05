using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio.ServiceInterface;

namespace WebApplication3.Servicio
{
    public  class ArticuloService : IServiceArticulo
    {
        private Context context;
        public ArticuloService()
        {
            context = Context.GetContext();
        }
        public List<Articulo> ArtsList()
        {
            return context.Articulos.Include(a=>a.categoria).ToList();
        }
        public Articulo FindArt(int? id) {
            if (id == null) { return null; }
            return context.Articulos.Find(id);
        }
        public List<Articulo> ArtsListIncludeCategory() {
            return
                 context.Articulos.Include(a => a.categoria).ToList();
        }
        public void ArtAdd(Articulo articulo) {
            context.Articulos.Add(articulo);
        }
        public Articulo ArtIncludeCategory(int? id) {
            if (id == null) return null;
            return context.Articulos.Include(a => a.categoria).Where(a => a.id == id).First();
        }
        public void ArtDrop(int? id ) {
            if (id != 0 || id != null) context.Articulos.Remove(context.Articulos.Find(id));
        }
        public List<Articulo> ArtsListInlcudeCategoryEqualsName(string query) {
            if (String.IsNullOrEmpty(query)) return (new List<Articulo>());
            return context.Articulos.Include(a => a.categoria).Where(a => a.nombre.Contains(query)).ToList();
        }
        public void SaveChanges() {
            context.SaveChanges();
        }
    }
}