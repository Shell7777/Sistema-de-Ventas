using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication3.Controllers;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio.ServiceInterface;

namespace WebAplication3.Test.ControllerTest
{
    [TestFixture]
    class ArticuloControllerTest
    {
        [Test]
        public void probarIndexRetornaArticulo()
        {
            var faker = new Mock<IServiceArticulo>();
            faker.Setup(o => o.ArtsList()).Returns(new List<Articulo>
            {
                new Articulo{ id = 1, idcategoria = 3, nombre = "Articulo1",codigo = "001", precio_venta = 10, stock = 5, descripcion ="asdqwe", condicion = true},
                new Articulo{ id = 2, idcategoria = 4, nombre = "Articulo2",codigo = "002", precio_venta = 10, stock = 5, descripcion ="asdqwe", condicion = true},

            });

            var controller = new ArticuloController(faker.Object);
            var view = controller.Index() as ViewResult;
            var model = view.Model as List<Articulo>;

            Assert.IsNotNull(view);
            Assert.IsInstanceOf<ViewResult>(view);
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public void probarIndexRetornaVacio()
        {
            var faker = new Mock<IServiceArticulo>();
            faker.Setup(o => o.ArtsList()).Returns(new List<Articulo>());

            var controller = new ArticuloController(faker.Object);
            var view = controller.Index() as ViewResult;
            var model = view.Model as List<Articulo>;

            Assert.IsInstanceOf<ViewResult>(view);
            Assert.AreEqual(0, model.Count);

        }

        [Test]
        public void probarBuscarArticuloRetorna()
        {
            var mock = new Mock<IServiceArticulo>();
            mock.Setup(o => o.ArtsListInlcudeCategoryEqualsName("Articulo1")).Returns(new List<Articulo>());

            var controller = new ArticuloController(mock.Object);
            var view = controller.Search_art("Articulo1") as ViewResult;

            Assert.IsNotNull(view.Model);
            Assert.IsInstanceOf<ViewResult>(view);
        }

        [Test]
        public void CreateTest()
        {
            Articulo articulo = new Articulo();
            var mock = new Mock<IServiceArticulo>();
            mock.Setup(o => o.ArtAdd(articulo));
            var controller = new ArticuloController(mock.Object);


            var view = controller.Create() as ViewResult;
            Assert.IsInstanceOf<ViewResult>(view);

        }

        //[Test]
        //public void probarDropArticuloRetorna()
        //{

        //}

        //[Test]
        //public void probarDeleteArticuloRetorna()
        //{
        //    int id = 1;
        //    var mock = new Mock<IServiceArticulo>();
        //    mock.Setup(o => o.FindArt(id).condicion).Returns(true);

        //    var controller = new ArticuloController(mock.Object);
        //    var view = controller.Delete(id) as ViewResult;

        //    Assert.IsNotNull(view.Model);
        //    Assert.IsInstanceOf<ViewResult>(view);
        //}

    }
}
