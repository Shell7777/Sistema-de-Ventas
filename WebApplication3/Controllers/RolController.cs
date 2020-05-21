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
    public class RolController : Controller
    {
        Context context = Context.GetContext();
        public ActionResult Index()
        {
            return View(context.Rol.ToList());
        }
        [HttpGet]
        public ActionResult Create() {
            
            return View(new Rol());
        }
        [HttpPost]
        public ActionResult Create(Rol rolView) {
            //ModelState.AddModelError("nombre", "sd");
            //ModelStateDictionary ModelState = mValidaciones.Validar_Rol(rolView);
            mValidaciones.Validar_Rol(rolView,
                ModelState);
            if (ModelState.IsValid)
            {
                context.Rol.Add(rolView);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rolView);
        
            
        }
        public ActionResult Delete(int? id) {
            if (id == null) RedirectToAction("Index");
           
            context.Rol.Remove(context.Rol.Find(id));
            context.SaveChanges();
            return RedirectToAction("Index");
        
        }
    }
}