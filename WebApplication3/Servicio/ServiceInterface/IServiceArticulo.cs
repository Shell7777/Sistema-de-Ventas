using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models.Clases;

namespace WebApplication3.Servicio.ServiceInterface
{
    public interface IServiceArticulo
    {
        List<Articulo> ArtsList();
        Articulo FindArt(int? id);
        List<Articulo> ArtsListIncludeCategory();
        void ArtAdd(Articulo articulo);
        Articulo ArtIncludeCategory(int? id);
        void ArtDrop(int? id);
        List<Articulo> ArtsListInlcudeCategoryEqualsName(string query);
        void SaveChanges();
    }

}
