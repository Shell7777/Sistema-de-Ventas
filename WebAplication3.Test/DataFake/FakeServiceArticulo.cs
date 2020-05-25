using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models.Clases;
using WebApplication3.Servicio.ServiceInterface;

namespace WebAplication3.Test.DataFake
{
    class FakeServiceArticulo : IServiceArticulo
    {
        public void ArtAdd(Articulo articulo)
        {
            throw new NotImplementedException();
        }

        public void ArtDrop(int? id)
        {
            throw new NotImplementedException();
        }

        public Articulo ArtIncludeCategory(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Articulo> ArtsList()
        {
            throw new NotImplementedException();
        }

        public List<Articulo> ArtsListIncludeCategory()
        {
            throw new NotImplementedException();
        }

        public List<Articulo> ArtsListInlcudeCategoryEqualsName(string query)
        {
            throw new NotImplementedException();
        }

        public Articulo FindArt(int? id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
