using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;
using Context = WebApplication3.Models.Context;

namespace WebApplication3.Controllers
{
    public class IngresoController : Controller
    {
        Context context = Context.GetContext();

        public JsonResult searchProduct(string query) {

            if (String.IsNullOrEmpty(query)) return Json(context.Articulos.ToList(),JsonRequestBehavior.AllowGet);
            var findProducts = context.Articulos.Where(a => a.nombre.Contains(query));
            ViewBag.consulta = query;
            return Json(findProducts, JsonRequestBehavior.AllowGet);
        }  
        public ActionResult Index()
        {
            return View(context.Articulos.ToList());
        }
        [HttpGet]
        public ActionResult AddNewArticulo () {
            return View();
        }
        [HttpPost]
        public ActionResult AddNewArticulo(Ingreso ingreso)
        {
            return View();
        }
        [HttpGet]
        public ActionResult addCountArticulo(int? idArtiuculo)
        {
            if (idArtiuculo != null) {
                return View (context.Articulos.Find(idArtiuculo));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult addCountArticulo(Ingreso ingreso) {
            return RedirectToAction("Index");
        }
        
        
        //bool pasoValicacion = EsValido(tema);   
        //if (tema.Nombre == null || tema.Nombre == "")
        //    ModelState.AddModelError("Nombre", "Nombre es obligatorio");
        //if (tema.Descripcion == null || tema.Descripcion == "")
        //    ModelState.AddModelError("Descripcion", "Descripcion es obligatorio...");
    }

}