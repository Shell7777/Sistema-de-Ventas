using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio;
using WebApplication3.Sources.Validations;

namespace WebApplication3.Controllers
{
    public class ArticuloController : Controller
    {
        Context context = Context.GetContext();
        
        private IService service;
        public ArticuloController(){}
        public ArticuloController(IService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View(context.Articulos.Include(a => a.categoria).ToList());
        }
        [HttpGet]
        public ActionResult Create() {
            ViewBag.categoria = context.Categorias.ToList();
            return View(new Articulo());
        }
        [HttpPost]
        public ActionResult Create(Articulo articulo) {
            if (mValidaciones.Validar_Articulo(articulo).IsValid) {
                context.Articulos.Add(articulo);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoria = context.Categorias.ToList();
            return View(articulo);
        }
        [HttpGet]
        public ActionResult Edit(int? id) {
            if (id != null) {
                ViewBag.cateoria = context.Categorias.ToList();
                return View(context.Articulos.Include(a => a.categoria).Where(a => a.id == id));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Articulo articulo)
        {
            if (mValidaciones.Validar_Articulo(articulo).IsValid) {
                context.Entry(articulo).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cateoria = context.Categorias.ToList();
            return View(articulo);

        }
        public ActionResult Delete(int? id) {
            if (id != null) {
                var product_desbled = context.Articulos.Find(id);
                product_desbled.condicion = false;
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public string Drop(int? id ) {
            if (id != null) {
                try
                {
                    context.Articulos.Remove(context.Articulos.Find(id));
                    context.SaveChanges();
                    return ("El producto fue borrado exitosamente");
                }
                catch (Exception e) {
                    return e.Message;
                }
                
            }
            return "Valor no Valido";
            
        }

        public ActionResult Search_art(string query) {
            if (String.IsNullOrEmpty(query)) return View(new List<Articulo>());
            ViewBag.Query = query;
            var listArticulos = context.Articulos
                                    .Include(a=>a.categoria)
                                    .Where(a => a.nombre.Equals(query))
                                    .ToList();
            
            return View(listArticulos);
        }

        public ActionResult listUsers() {
          // var service = new Service();
            var usuarioList = service.articulosList();
            return View(usuarioList);
        }

    }
}