
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio;
using WebApplication3.Servicio.ServiceClass;
using WebApplication3.Servicio.ServiceInterface;
using WebApplication3.Sources.Validations;

namespace WebApplication3.Controllers
{
    public class CategoriaController : Controller
    {
        private IServiceCategoria service;

        public CategoriaController()
        {
            service = new CategoriaService();
        }
        public CategoriaController(IServiceCategoria service)
        {
            this.service = service;
        }
        public ActionResult Index()
        {
            return View(service.CatsList());
        }
        [HttpGet]
        public ActionResult Create() {
            return View(new Categoria());
        }
        [HttpPost]
        public ActionResult Create(Categoria categoriaView) {
            validar_Create(categoriaView);
            if (ModelState.IsValid) {
                service.CatAdd(categoriaView);
                service.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaView);
        }
        [HttpGet]
        public ActionResult Edit(int? id ) {
            if (id != null) return View( service.CatFind(id));
            
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Categoria categoria) {
            validar_Edit(categoria);
            if (ModelState.IsValid) {

                var categoriaBD = service.CatFind(categoria.id);
                categoriaBD.descripcion = categoria.descripcion;
                categoriaBD.condicion = categoria.condicion;
                service.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult SearchCategory(string query) {
            ViewBag.query = query;
            var results = service.CatsListEqualsName(query);
            return View(results);
        }
        [HttpGet]
        public ActionResult Delete(int? id) {
            if (id == null) return RedirectToAction("Index");
            var valor = service.CatFind(id);
            valor.condicion = !valor.condicion;
            return RedirectToAction("Index");
        }

        public void validar_Create(Categoria categoria)
        {
            if (String.IsNullOrEmpty(categoria.nombre))
            {
                ModelState.AddModelError("nombre", "*Ingrese un nombre");
            }
            if (String.IsNullOrEmpty(categoria.descripcion))
            {
                ModelState.AddModelError("descripcion", "*Ingrese una descripcion");
            }
        }
        public void validar_Edit(Categoria categoria) {
            if (String.IsNullOrEmpty(categoria.descripcion))
            {
                ModelState.AddModelError("descripcion", "*Ingrese una descripcion");
            }
        }
    }
}