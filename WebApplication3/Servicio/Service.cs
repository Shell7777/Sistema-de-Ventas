
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models;
using WebApplication3.Models.Clases;

namespace WebApplication3.Servicio.ServiceClass
{
    public class Service:IService
    {
        private Context context;
        public Service()
        {
            this.context = Context.GetContext();
        }
        public List<Articulo> articulosList() {
            return context.Articulos.ToList();
        }
    }
    public interface IService
    {
        List<Articulo> articulosList();
    }
}