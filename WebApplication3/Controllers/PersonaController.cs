using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.Clases;

namespace WebApplication3.Controllers
{
    public class PersonaController : Controller
    {
        Context context =  Context.GetContext();
        public ActionResult Index()
        {
            return View(context.Personas.ToList());
        }
        [HttpGet]
        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Persona persona)
        {

            context.Personas.Add(persona);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}