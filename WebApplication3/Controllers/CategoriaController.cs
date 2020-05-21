
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;
using WebApplication3.Sources.Validations;

namespace WebApplication3.Controllers
{
    public class CategoriaController : Controller
    {
        Context context = Context.GetContext();
        public ActionResult Index()
        {
            return View(context.Categorias.ToList()); 
        }
        [HttpGet]
        public ActionResult Create() {
            return View(new Categoria());
        }
        [HttpPost]
        public ActionResult Create(Categoria categoriaView) {
            context.Categorias.Add(categoriaView);
            context.SaveChanges();
            return RedirectToAction("Index");

            /* if (mValidaciones.Validar_Categoria(categoriaView).IsValid) {
                 context.Categorias.Add(categoriaView);
                 context.SaveChanges();
                 return RedirectToAction("Index");
             }
             return View(categoriaView);*/
        }
        [HttpGet]
        public ActionResult Edit(int? id ) {
            if (id != null) return View( context.Categorias.Find(id));

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Categoria categoria) {
            var categoriaBD = context.Categorias.Find(categoria.id);
            categoriaBD.descripcion = categoria.descripcion;
            categoriaBD.condicion = categoria.condicion;
            context.SaveChanges();
            return RedirectToAction("Index");
            /*if (mValidaciones.Validar_Categoria(categoria).IsValid) {
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria);*/

        }
        public ActionResult SearchCategory(string query) {
            if (String.IsNullOrEmpty(query)) return View(new List<Categoria>());
            var results = context.Categorias.Where(a => a.nombre.Equals(query)).ToList();
            ViewBag.query = query;
            return View(results);
        }
        [HttpGet]
        public ActionResult Delete(int? id) {
            if (id == null) return RedirectToAction("Index");
            var valor = context.Categorias.Find(id);
            valor.condicion = !valor.condicion;
            return RedirectToAction("Index");
        }
    }
}