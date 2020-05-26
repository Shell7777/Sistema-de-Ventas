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
        public void testEditNoSeaNullIDGet() {
            //enviar un id null
            var mocky = new Mock<IServiceCategoria>();
            var controller = new CategoriaController(mocky.Object);
            int? valor = null;
            var view = (RedirectToRouteResult)controller.Edit(valor)  ;
            Assert.IsInstanceOf<RedirectToRouteResult>(view);            
        }
        [Test]
        public void testEditExitosGet() {
            // Encontrar satisfactoriamente y regresa una vista con modelo
            var categoriaPrueba = new Categoria();
            var mocky = new Mock<IServiceCategoria>();
            mocky.Setup(a => a.CatFind(8)).Returns(categoriaPrueba);
            var controller = new CategoriaController(mocky.Object);
            var view = (ViewResult)controller.Edit(8);
            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsNotNull(view.Model);
        }
        [Test]
        public void testEditFallidoPost()
        {
            //crear un categoria que falle 
            var categoriaPrueba = new Categoria();
            var mocky = new Mock<IServiceCategoria>();
            var controller = new CategoriaController(mocky.Object);
            var view = controller.Edit(categoriaPrueba) as ViewResult;
            Assert.IsInstanceOf<ViewResult>(view);
            Assert.IsNotNull(view.Model);
            //Assert.AreEqual(view.ViewName,"Edit");
        }
        [Test]
        public void testEditExitosoPost()
        {
            //crear un categoria que se guarde de forma exitosa 
            var categoriaPrueba = new Categoria();
            categoriaPrueba.descripcion = "lorem impsum lorem impsum lorem impsum";
            categoriaPrueba.nombre = "Embutidos";
            var mocky = new Mock<IServiceCategoria>();
            mocky.Setup(a => a.CatFind(5)).Returns(categoriaPrueba);
            mocky.Setup(a => a.SaveChanges());
            var controller = new CategoriaController(mocky.Object);
            var categoriaView = new Categoria();
            categoriaView.id = 5;
            categoriaView.nombre = "Lacteos";
            categoriaView.descripcion = "leche de la vaca lola";
            var view = controller.Edit(categoriaView) as RedirectToRouteResult;
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
 
        }
        [Test]
        public void testIDDeleteExitoso() {
            var mocky = new Mock<IServiceCategoria>();
            mocky.Setup(a => a.CatFind(5)).Returns(new Categoria { condicion = true});
            var controller = new CategoriaController(mocky.Object);
            var view = (RedirectToRouteResult)controller.Delete(5);
            Assert.IsInstanceOf<RedirectToRouteResult>(view);
        }
        [Test]
        public void testSearchIsWithNull()
        {
            var mocky = new Mock<IServiceCategoria>();
            mocky.Setup(a => a.CatsListEqualsName(null))
                    .Returns(new List<Categoria>());
            var controller = new CategoriaController(mocky.Object);
            var view = (ViewResult)controller.SearchCategory(null);
            Assert.IsNull(view.ViewBag.query);
            Assert.IsNotNull(view.Model);
        }
        [Test]
        public void testSearchConNullQuery()
        {
            var listaCat = new List<Categoria>();
            listaCat.Add(new Categoria());
            listaCat.Add(new Categoria());
            var mocky = new Mock<IServiceCategoria>();
            mocky.Setup(a => a.CatsListEqualsName("Valor"))
                    .Returns(listaCat);
            var controller = new CategoriaController(mocky.Object);
            var view = (ViewResult)controller.SearchCategory("Valor");
            Assert.IsNotNull(view.ViewBag.query);
            Assert.AreEqual(view.ViewBag.query,"Valor");
            Assert.IsNotNull(view.Model);
            var model = (List<Categoria>)view.Model;
            Assert.AreEqual(model.Count,2);
        }
    }
}
