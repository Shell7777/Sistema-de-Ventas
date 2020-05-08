using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;

namespace WebApplication3.Controllers
{
    public class VentaController : Controller
    {
        Context context = Context.GetContext();
        public ActionResult Index()
        {
            return View(context.ventas.ToList());
        }
        [HttpGet]
        public ActionResult Create() {
            ViewBag.usuario = context.Usuarios.ToList();
            ViewBag.cliente = context.Personas.ToList();


            return View(context.Articulos.Include(a=>a.categoria).Where(a=>a.condicion==true).ToList());

        }
        public ActionResult AgregarArticulo(int id) {
            var articulo = context.Articulos.FirstOrDefault(o => o.id == id);
            return PartialView(articulo);
        }
        [HttpPost]
        public ActionResult Create(List<Detalle_Venta> store_list,Venta store)
        {

            return RedirectToAction("Index");
        }
        public int?  ReturnStock_Product(int? id ) {
            if (id != null) {
                var isProductFind = context.Articulos.Find(id);
                return isProductFind.stock;  
            }
            return null;
        }
    }
}