using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAplication3.Test.DataFake;
using WebApplication3.Controllers;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio;
using WebApplication3.Servicio.ServiceInterface;

namespace WebAplication3.Test.ControllerTest
{
   [TestFixture]
    class ArtiucloControllerTest
    {
        [Test]
        public void Test_index_Include_to_Categoria() {
            var ArticuloView = new ArticuloController();

            var IncludeCat = ArticuloView.Index() as ViewResult;
            var ArticuloModel = IncludeCat.Model as Articulo;
            Assert.IsNotNull(ArticuloModel.categoria);
        }
        [Test]
        public void Test_index_is_not_null()
        {
            var ArticuloView = new ArticuloController();
            
            Assert.IsNotNull(ArticuloView.Index());
        }

        [Test]
        public void Test_listArticulo()
        {
            var mock = new Mock<IServiceArticulo>();
            mock.Setup(a => a.ArtsList()).Returns(new List<Articulo>());
            var mcontroller = new ArticuloController(mock.Object);
        }
        [Test]
        public void Test_listUsers_1() {
            var mcontroller = new ArticuloController(new FakeServiceArticulo());
            var view = mcontroller.Delete(8);
            Assert.IsInstanceOf<ViewResult>(view);
        }
    }
}
