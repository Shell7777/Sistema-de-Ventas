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
    public class ArticuloController : Controller
    {

        private IServiceArticulo service;
        private IServiceCategoria serviceCategoria;
        public ArticuloController(){
            service = new ArticuloService();
            serviceCategoria = new CategoriaService();
        }
        public ArticuloController(IServiceArticulo service)
        {
            this.service = service;
        }
        public ArticuloController(IServiceArticulo service,IServiceCategoria serviceCategoria)
        {
            this.service = service;
            this.serviceCategoria = serviceCategoria;
        }

        public ActionResult Index()
        {
            return View(service.ArtsList());
        }
        [HttpGet]
        public ActionResult Create() {
            ViewBag.categoria = service.ArtsListIncludeCategory();
            return View(new Articulo());
        }
        [HttpPost]
        public ActionResult Create(Articulo articulo) {
            if (mValidaciones.Validar_Articulo(articulo).IsValid) {
                service.ArtAdd(articulo);
                service.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoria = serviceCategoria.CatsList();
            return View(articulo);
        }
        [HttpGet]
        public ActionResult Edit(int? id) {
            if (id != null) {
                ViewBag.cateoria = serviceCategoria.CatsList();
                var valor = service.ArtIncludeCategory(id);
                return View(valor);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Articulo articulo)
        {
            //if (mValidaciones.Validar_Articulo(articulo).IsValid) {
                var articuloBD = service.FindArt(articulo.id);
                articuloBD.nombre = articulo.nombre;
                articuloBD.codigo = articulo.codigo;
                articuloBD.precio_venta= articulo.precio_venta;
                articuloBD.descripcion = articulo.descripcion;
                articuloBD.condicion = articulo.condicion;

                //context.Entry(articulo).State = EntityState.Modified;
                service.SaveChanges();
                return RedirectToAction("Index");
            //}
            //ViewBag.cateoria = serviceCategoria.CatsList();
            //return View(articulo);

        }
        public ActionResult Delete(int? id) {
            if (id != null) {
                var product_desbled = service.FindArt(id);
                product_desbled.condicion = !product_desbled.condicion;
            }
            service.SaveChanges();
            return RedirectToAction("Index");
        }
        public string Drop(int? id ) {
            if (id != null) {
                try
                {
                    service.ArtDrop(id);
                    return ("El producto fue borrado exitosamente");
                }
                catch (Exception e) {
                    return e.Message;
                }
                
            }
            return "Valor no Valido";
            
        }

        public ActionResult Search_art(string query) {
            
            ViewBag.Query = query;
            var listArticulos = service.ArtsListInlcudeCategoryEqualsName(query);
            return View(listArticulos);
        }
        public ActionResult listUsers() {
            var usuarioList = service.ArtsList();
            return View(usuarioList);
        }

    }
}