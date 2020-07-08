
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;

namespace WebApplication3.Controllers
{
    public class AlmacenController : Controller
    {
        // GET: Almacen


        Context contex = Context.GetContext();
        public ActionResult listaIngreso() {
            var valor = contex.Ingresos.Include(a => a.Proveedor).Include(a => a.Usuario).ToList();
            return View(valor);
        }
        [HttpGet]
        public ActionResult CreateArticuloNuevo()
        {
            ViewBag.proveedor = contex.Personas.Where(a => a.tipo_persona == "1").ToList();
            ViewBag.categoria = contex.Categorias.Where(a => a.condicion).ToList();
            return View(new Articulo());
        }
        public bool SiesUnico(string nombre)
        {
            return contex.Articulos.Any(a => a.nombre == nombre);
        }
        [HttpPost]
        public ActionResult CreateArticuloNuevo(Ingreso ingreso)
        {
            llenarDatos(ingreso);
            validar_Ingreso(ingreso);
            if (ModelState.IsValid) {
                contex.Ingresos.Add(ingreso);
                contex.SaveChanges();
                return RedirectToAction("listaIngreso");
            }
            ViewBag.proveedor = contex.Personas.Where(a => a.tipo_persona == "1").ToList();
            ViewBag.categoria = contex.Categorias.Where(a => a.condicion).ToList();
            return View(ingreso);
        }
        public ActionResult BuscarProveedor(string query)
        {
            ViewBag.query = query;
            if (String.IsNullOrWhiteSpace(query) || String.IsNullOrEmpty(query))
            {
                return View(contex.Personas.Where(a => a.tipo_persona == "1").ToList());
            }
            return View(contex.Personas.Where(a => a.tipo_persona == "1" && a.nombre.Contains(query)).ToList());
        }
        public ActionResult DetailsProveedor(int? id)
        {
            if (id == null) return RedirectToAction("buscarProveedor");
            var ingresos = contex.Personas.Include(a=>a.Ingresos).Where(a => a.id == id).FirstOrDefault();
            if (ingresos == null) return RedirectToAction("buscarProveedor");

            return View(ingresos);
        }
        public ActionResult DetailsdeIngresos(int? id)
        {
            if (id == null) return RedirectToAction("buscarProveedor");
            var ingresos = contex.Detalle_Ingresos.Include(a=>a.Ingreso).Include(a => a.Articulo).Where(a => a.Ingreso.id == id).FirstOrDefault();
            if (ingresos == null) return RedirectToAction("buscarProveedor");
            ViewBag.idProveedor = ingresos.Ingreso.idproveedor;
            return View(ingresos);
        }
        public ActionResult DeleteProveedor(int? id ) {
            if (id != null) contex.Personas.Remove(contex.Personas.Find(id));
            return RedirectToAction("BuscarProveedor");
        
        }
        [HttpGet]
        public ActionResult AgregarProducto()
        {
            ViewBag.proveedor = contex.Personas.Where(a => a.tipo_persona == "1");
            ViewBag.articulos = contex.Articulos.Where(a => a.condicion).ToList();
            return View(new Ingreso());
        }
        [HttpPost]
        public ActionResult AgregarProducto(Ingreso ingreso)
        {
            llenardatos_agregarProducto(ingreso);
            validar_agregar_producto(ingreso);
            
            if (ModelState.IsValid) {
                foreach (var detalle in ingreso.detalle_Ingresos) {
                    var det = contex.Articulos.Where(a => a.id == detalle.idarticulo).FirstOrDefault();
                    det.stock += detalle.cantidad;
                }
                contex.Ingresos.Add(ingreso);
                contex.SaveChanges();
                return RedirectToAction("listaIngreso");
            }

            ViewBag.proveedor = contex.Personas.Where(a => a.tipo_persona == "1");
            ViewBag.articulos = contex.Articulos.Where(a => a.condicion).ToList();
            return View(ingreso);
        }
        [HttpGet]
        public ActionResult CreateProveedor()
        {
            return View(new Persona()   );
        }
        [HttpPost]
        public ActionResult CreateProveedor(Persona proveedor)
        {
            validar_Proveedor(proveedor);
            if (ModelState.IsValid)
            {
                proveedor.tipo_persona = "1";
                contex.Personas.Add(proveedor);
                contex.SaveChanges();
                return RedirectToAction("BuscarProveedor");
            }
            return View(proveedor);
        }
        public void validar_Proveedor(Persona proveedor)
        {
            if (String.IsNullOrEmpty(proveedor.nombre))
            {
                ModelState.AddModelError("nombre", "*Ingrese el campo");
            }
            if (String.IsNullOrEmpty(proveedor.direccion))
            {
                ModelState.AddModelError("direccion", "*Ingrese el campo");
            }

            if (String.IsNullOrEmpty(proveedor.email) || !validarEmail(proveedor.email))
            {
                ModelState.AddModelError("email", "*Ingrese el campo");
            }
            if (String.IsNullOrEmpty(proveedor.telefono) || proveedor.telefono.Length != 9)
            {
                ModelState.AddModelError("telefono", "*Ingrese el campo");
            }
            if (String.IsNullOrEmpty(proveedor.num_documento))
            {
                ModelState.AddModelError("num_documento", "*Ingrese el campo");
            }
            if (String.IsNullOrEmpty(proveedor.tipo_documento))
            {
                ModelState.AddModelError("tipo_documento", "*Ingrese el campo");
            }

        }
        public void validar_agregar_producto(Ingreso ingreso)
        {
            if (String.IsNullOrEmpty(ingreso.tipo_comprobante))
            {
                ModelState.AddModelError("tipo_comprobante", "*Ingrese el tipo de comprobante");
            }
            if (String.IsNullOrEmpty(ingreso.num_comprobante))
            {
                ModelState.AddModelError("num_comprobante", "*Ingrese el numero de comprobante");
            }
            if (ingreso.idproveedor <= 0)
            {
                ModelState.AddModelError("idproveedor", "*Ingrese provedor");
            }
            if (ingreso.idusuario <= 0)
            {
                ModelState.AddModelError("idusuario", "*Debe Logiarse");
            }
            if (ingreso.detalle_Ingresos.Count <= 0 || ingreso.detalle_Ingresos == null)
            {
                ModelState.AddModelError("idusuario", "*Debe Logiarse");
            }

        }
        public static bool validarEmail(string email)
        {
            String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            return Regex.IsMatch(email, expresion);
        }
        public void llenardatos_agregarProducto(Ingreso ingreso) {
            ingreso.estado = "Activo";
            ingreso.idusuario = 1;
            decimal total = 0;
            ingreso.num_comprobante = "00484";
            ingreso.serie_comprobante = "45DSD3";
            ingreso.tipo_comprobante = "Codigo de barras";
            ingreso.fecha_hora = DateTime.Now;
            foreach (var detalle in ingreso.detalle_Ingresos)
            {
                total = total + (detalle.precio * ((decimal)detalle.cantidad));
            }
            ingreso.total = total;
            ingreso.impuesto = total * (decimal)0.15;
        }
        public void validar_Ingreso(Ingreso  ingreso)
        {
            
            if (String.IsNullOrEmpty(ingreso.tipo_comprobante))
            {
                ModelState.AddModelError("tipo_comprobante", "*Ingrese el tipo de comprobante");
            }
            if (String.IsNullOrEmpty(ingreso.num_comprobante))
            {
                ModelState.AddModelError("num_comprobante", "*Ingrese el numero de comprobante");
            }
            if (ingreso.idproveedor <= 0 )
            {
                ModelState.AddModelError("idproveedor", "*Ingrese provedor");
            }
            if (ingreso.idusuario <= 0)
            {
                ModelState.AddModelError("idusuario", "*Debe Logiarse");
            }
            if (ingreso.detalle_Ingresos.Count <= 0 || ingreso.detalle_Ingresos == null)
            {
                ModelState.AddModelError("idusuario", "*Debe Logiarse");
            }
            else {
                foreach (var detatalle in ingreso.detalle_Ingresos)
                {
                    validar_Producto(detatalle.Articulo);
                    if (SiesUnico(detatalle.Articulo.nombre)) { ModelState.AddModelError("articulo", "*Hay un articulo existente"); }
                }
            }

        }
        public void llenarDatos(Ingreso ingreso) {
            ingreso.estado = "Activo"  ;
            ingreso.idusuario = 1 ;
            decimal total = 0;
            ingreso.num_comprobante = "00484";
            ingreso.serie_comprobante = "45DSD3";
            ingreso.tipo_comprobante = "Codigo de barras";
            ingreso.fecha_hora = DateTime.Now;
            foreach (var detalle in ingreso.detalle_Ingresos)
            {
                detalle.Articulo.codigo = ingreso.serie_comprobante;
                detalle.Articulo.stock = detalle.cantidad;
                detalle.Articulo.precio_venta = detalle.precio + (detalle.precio * (decimal)0.15);
                total = total + (detalle.precio * ((decimal) detalle.cantidad  ));
            }
            ingreso.impuesto = total * (decimal)0.15;
       
            ingreso.total=total;
            
        }
        public void validar_Producto(Articulo articulo)
        {
            if (string.IsNullOrEmpty(articulo.nombre))
            {
                ModelState.AddModelError("nombre", "*Ingrese nombre");
            }

            if (string.IsNullOrEmpty(articulo.codigo))
            {
                ModelState.AddModelError("codigo", "*Ingrese un código válido ");
            }
            if (string.IsNullOrEmpty(articulo.descripcion))
            {
                ModelState.AddModelError("descripcion", "*Ingrese una descripción");
            }
            if (articulo.precio_venta == null || articulo.precio_venta < 0)
            {
                ModelState.AddModelError("precioventa", "*Ingrese una precio valido");
            }
            if (articulo.stock == null || articulo.stock < 0)
            {
                ModelState.AddModelError("stock", "*Ingrese un stock válido");
            }
            if (articulo.idcategoria == null || articulo.idcategoria< 0)
            {
                ModelState.AddModelError("precioventa", "*Ingrese una categoria valida");
            }
        }
    }
}
