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
            
            return View(new Persona());

        }
        [HttpPost]
        public ActionResult Create(Persona persona)
        {
            validar_Create(persona);
            
            if (ModelState.IsValid)
            {

                context.Personas.Add(persona);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);
            
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null) return View(context.Personas.Find(id));

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Persona persona)
        {
            validar_Edit(persona);
            if (ModelState.IsValid)
            {

                var personaBD = context.Personas.Find(persona.id);
                personaBD.nombre= persona.nombre;
                personaBD.direccion = persona.direccion;
                personaBD.telefono = persona.telefono;
                personaBD.email = persona.email;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(persona);

        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            var valor = context.Personas.Find(id);
            context.Personas.Remove(valor);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public void validar_Create(Persona persona)
        {
            if (String.IsNullOrEmpty(persona.nombre))
            {
                ModelState.AddModelError("nombre", "*Ingrese un nombre");
            }
            if (String.IsNullOrEmpty(persona.tipo_persona))
            {
                ModelState.AddModelError("tipo_persona", "*Ingrese un tipo de persona");
            }
            if (String.IsNullOrEmpty(persona.tipo_documento))
            {
                ModelState.AddModelError("tipo_documento", "*Ingrese un tipo de documento");
            }
            if (String.IsNullOrEmpty(persona.num_documento))
            {
                ModelState.AddModelError("num_documento", "*Ingrese un número de documento");
            }
            if (String.IsNullOrEmpty(persona.direccion))
            {
                ModelState.AddModelError("direccion", "*Ingrese un telefono");
            }
            if (String.IsNullOrEmpty(persona.telefono))
            {
                ModelState.AddModelError("telefono", "*Ingrese un telefono");
            }
            if (String.IsNullOrEmpty(persona.email))
            {
                ModelState.AddModelError("email", "*Ingrese un email");
            }
        }
        public void validar_Edit(Persona persona)
        {
            if (String.IsNullOrEmpty(persona.nombre))
            {
                ModelState.AddModelError("nombre", "*Ingrese una descripcion");
            }
            if (String.IsNullOrEmpty(persona.direccion))
            {
                ModelState.AddModelError("direccion", "*Ingrese un telefono");
            }
            if (String.IsNullOrEmpty(persona.telefono))
            {
                ModelState.AddModelError("telefono", "*Ingrese un telefono");
            }
            if (String.IsNullOrEmpty(persona.email))
            {
                ModelState.AddModelError("email", "*Ingrese un email");
            }
        }
    }
}