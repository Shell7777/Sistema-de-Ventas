using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;
using WebApplication3.Sources.Validations;

namespace WebApplication3.Controllers
{
    public class UsuarioController : Controller
    {
        Context context = Context.GetContext();
        public ActionResult Index()
        {
            return View(context.Usuarios.Include(a=>a.roll).ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.rol = context.Rol.ToList();
            return View(new Usuario());
        }
        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {

            validar(usuario);
            ViewBag.rol = context.Rol.ToList();
            if (ModelState.IsValid)
            {
                usuario.tipo_documento = "DNI";

                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(usuario);
        }
        public ActionResult EstadoUsuario(int? id) {
            if (id != null || id > 0) {
                var usuario = context.Usuarios.Find(id);
                usuario.condicion = !usuario.condicion;
                context.SaveChanges();
             }
            return RedirectToAction("Index");
        }
        public ActionResult buscarUsuario (string nombre )
        {
            var usuario = context.Usuarios.Include(a => a.roll).Where(a => a.nombre.Contains(nombre)).ToList();

            return View(usuario);
        }
        public void validar(Usuario usuario)
        {
         
            if (String.IsNullOrEmpty(usuario.direccion)) ModelState.AddModelError("direccion", "*Ingrese una dirección válida");
            
            if (String.IsNullOrEmpty(usuario.password_salt) || usuario.password_salt.Length > 6) ModelState.AddModelError("password_salt", "*Ingrese una password válida");

            if (String.IsNullOrEmpty(usuario.nombre)) ModelState.AddModelError("nombre", "*Ingrese una dirección válida");
            
            if (usuario.idrol<=0) ModelState.AddModelError("Rol", "*Rol no validp");
            if (String.IsNullOrEmpty(usuario.num_documento)) ModelState.AddModelError("num_documento", "*No valido");

            if (String.IsNullOrEmpty(usuario.telefono)|| usuario.telefono.Length != 9) ModelState.AddModelError("telefono", "*No valido");


            if (String.IsNullOrEmpty(usuario.email) || validarEmail(usuario.email) == false)
            {
                ModelState.AddModelError("email", "*Ingrese un email válido");
            }

        }
        public static bool validarEmail(string email)
        {
            String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            return Regex.IsMatch(email, expresion);
        }

    } 
    
}