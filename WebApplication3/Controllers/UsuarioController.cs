using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(context.Usuarios.ToList());
        }
        [HttpGet]
        public ActionResult Create() {
            ViewBag.rol = context.Rol.ToList();
            return View(new Usuario());       
        }
        [HttpPost]
        public ActionResult Create(Usuario usuario)  {
            //            ModelState modelState = new ModelState();
            ViewBag.rol = context.Rol.ToList();
            if (mValidaciones.Validar_Usuario(usuario).IsValid) {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(usuario);
        }
    }
}