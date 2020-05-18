using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio;

namespace WebAplication3.Test.DataFake
{
    class ServiceFaker : IService
    {
        public List<Articulo> articulosList()
        {
            return new List<Articulo>(); 
        }
    }
}
