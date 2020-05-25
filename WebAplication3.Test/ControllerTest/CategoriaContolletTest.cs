using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using WebApplication3.Controllers;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio.ServiceInterface;

namespace WebAplication3.Test.ControllerTest
{
    //10 pruebas 
    [TestFixture]
    class CategoriaContolletTest
    {
        [Test]
        public void testCategorianotNultViewIndex()
        {
            //verficamos que el modelo se envie en la vista y si este es una lista 
            var mocky = new Mock<IServiceCategoria>();
            mocky.Setup(a => a.CatsList()).Returns(new List<Categoria>());
            var controller = new CategoriaController(mocky.Object);
            var view = (ViewResult)controller.Index();
            Assert.IsNotNull(view.Model);
            Assert.IsInstanceOf<List<Categoria>>(view.Model);

        }
        [Test]
        public void testCreateExitoso() {
            var categoriaObjectTest = new Categoria();
            categoriaObjectTest.nombre = "Lacteos";
            categoriaObjectTest.descripcion = "lorum lorun parse";
            var mocky = new Mock<IServiceCategoria>();
            mocky.Setup(a => a.CatAdd(categoriaObjectTest));
            var controller = new CategoriaController(mocky.Object);
            var view = controller.Create(categoriaObjectTest) as RedirectToRouteResult;
            Assert.IsNotNull(view);
        }
        [Test]
        public void testCreateFallidoValidacion()
        {
            var categoriaObjectTest = new Categoria();
            var mocky = new Mock<IServiceCategoria>();
            var controller = new CategoriaController(mocky.Object);
            var view = controller.Create(categoriaObjectTest) as ViewResult;
            Assert.IsNotNull(view.Model);
        }
        [Test]
        public void testEditNoSeaNullID() {
            var mocky = new Mock<IServiceCategoria>();
            var controller = new CategoriaController(mocky.Object);
            int? valor = null;
            var view = (RedirectToRouteResult)controller.Edit(valor)  ;
            Assert.IsInstanceOf<RedirectResult>(view);
            Assert.AreEqual(view.RouteName , "Index");
        }
        [Test]
        public void testEditExitos() { 
        }
        [Test]
        public void testEditFallido()
        {
        }

        [Test]
        public void testIDDeleteExitoso() { 
        
        }
        [Test]
        public void testIDDeleteFallido()
        {

        }

    }
}
