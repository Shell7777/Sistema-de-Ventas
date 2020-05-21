using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio.Loggin;

namespace WebApplication3.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult IniciarSession()
        {
            return View(new UserLogged());
        }

        [HttpPost]
        public ActionResult IniciarSession(UserLogged usuario)
        {
            Session["User"] = usuario;
            FormsAuthentication.SetAuthCookie(usuario.nombre, false);
            return RedirectToAction("Index");
            
        }
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.usuario = SessionInciada().nombre;
            return View();
        }

        [Authorize]
        private UserLogged SessionInciada()
        {
            return (UserLogged)Session["User"];
        }
    }
}
