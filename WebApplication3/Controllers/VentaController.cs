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
        public ActionResult Index() {

            return View(context.ventas.Include(a => a.Usuario).Include(a => a.Cliente).ToList());
        }
        [HttpGet]
        public ActionResult search()
        {
            ViewBag.fechaMaxima = DateTime.Today.ToString("yyyy-MM-dd");
            return View(new List<Venta>());
        }
        void validaFecha(DateTime? dateQuery) {
            if (dateQuery == null) ModelState.AddModelError("fecha", "*El valor ingresado no es valido");
            else {
                var dateConsulta = DateTime.Parse(dateQuery.ToString());
                if (dateConsulta.CompareTo(DateTime.Today) > 0) ModelState.AddModelError("fecha", "*El valor ingresado no es valido");
            }

        }
        [HttpPost]
        public ActionResult search(DateTime? dateQuery)
        {
            ViewBag.fechaMaxima = DateTime.Today.ToString("yyyy-MM-dd");
            validaFecha(dateQuery);
            ViewBag.dataquery = (dateQuery != null)
                    ? DateTime.Parse(dateQuery.ToString()).ToString("yyyy-MM-dd")
                    : "";
            if (ModelState.IsValid) {

                var dateConsulta = DateTime.Parse(dateQuery.ToString());
                var valor = dateConsulta.ToString("yyyy-MM-dd") as object;
                var ventasList = context.ventas.Include(a=>a.Usuario).Where(a => a.fecha_hora.CompareTo(dateConsulta) < 0).ToList();
                return View(ventasList);
            }
            return View();
        }
        [HttpGet]
        public ActionResult Create() {
            ViewBag.usuario = context.Usuarios.ToList();
            ViewBag.cliente = context.Personas.ToList();

            ViewBag.comprobante = "";
            return View(context.Articulos.Include(a=>a.categoria).Where(a=>a.condicion==true && a.stock>0).ToList());

        }
        public ActionResult AgregarArticulo(int id) {
            var articulo = context.Articulos.FirstOrDefault(o => o.id == id);
            return PartialView(articulo);
        }
        [HttpPost]
        public ActionResult Create(Venta store)
        {
            validarVenta(store);
            if (ModelState.IsValid)
            {
                store.total = 0m;
                foreach (var detalle in store.detalle_venta)
                {
                    detalle.precio = context.Articulos.Find(detalle.idarticulo).precio_venta * detalle.cantidad;
                    detalle.descuento = 0;
                    store.total += detalle.precio;
                }
                store.impuesto = store.total * 0.18m;
                store.fecha_hora = DateTime.Now;
                store.estado = "Exitoso";
                store.idcliente = 1;
                store.idusuario = 1;
                store.serie_comprobante = store.idusuario.ToString() + store.idcliente + "vent"+store.fecha_hora.ToString();
                context.ventas.Add(store);
                context.SaveChanges();
                actualizaRegisrosProductosBD(store.detalle_venta);
                return RedirectToAction("search");
            }
            ViewBag.comprobante = store.tipo_comprobante;
            ViewBag.numero = store.num_comprobante;
            return View(context.Articulos.Include(a => a.categoria).Where(a => a.condicion == true).ToList());

        }
        public void actualizaRegisrosProductosBD(List<Detalle_Venta> productosComprados)
        {
            foreach (var productComprado in productosComprados)
            {
                var productoBD = context.Articulos.Find(productComprado.idarticulo);
                if (productoBD.stock >= productComprado.cantidad  && productoBD.stock > 0) {
                    productoBD.stock -= productComprado.cantidad;
                }
            }
            context.SaveChanges();

        }
        public int?  ReturnStock_Product(int? id ) {
            if (id != null) {
                var isProductFind = context.Articulos.Find(id);
                return isProductFind.stock;  
            }
            return null;
        }
        public void validarVenta(Venta venta)
        {
            if (String.IsNullOrEmpty(venta.tipo_comprobante)) {
                ModelState.AddModelError("tipo_comprobante", "Ingrese el tipo de comprobante");
            }
            if (String.IsNullOrEmpty(venta.num_comprobante))
            {
                ModelState.AddModelError("num_comprobante", "Ingrese el numero de comprobante");
            }
            if (venta.detalle_venta == null || venta.detalle_venta.Count == 0) {

                ModelState.AddModelError("lista", "La lista está vacia");
                
            }
            
            //if (venta.idcliente == null) ModelState.AddModelError("cliente", "Registre un cliente");
           // if (venta.Usuario == null) ModelState.AddModelError("usuario", "Registre un cliente");
        }
    }
}