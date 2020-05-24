using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models.Clases;

namespace WebApplication3.Servicio.ServiceInterface
{
   public  interface IServiceCategoria
    {
        List<Categoria> CatsList();

        void SaveChanges();
        void CatAdd(Categoria categoria);

        Categoria CatFind(int? id);
        List<Categoria> CatsListEqualsName(string query);
        void CatDrop(int? id);
    }
}
