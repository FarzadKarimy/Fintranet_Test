using Fintranet_Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fintranet_Test.Test.Test
{
    public class moqData
    {

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>()
            {
                 new Product { Id = 1 , name="iphone x" , price = 15000},
                 new Product { Id = 2 , name="iphone xx" , price = 1000}
            };
            return products;
        }
    }
}
