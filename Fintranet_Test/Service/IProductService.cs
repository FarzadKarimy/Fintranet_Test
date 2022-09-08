using Fintranet_Test.Domain.Entities;
using System.Collections.Generic;

namespace Fintranet_Test.Service
{
    public interface IProductService
    {

        Product AddProduct(Product product);

        List<Product> GetProduct();

        void UpdateProduct(Product product);

        void DeleteProduct(int Id);

        Product GetProduct(int Id);
    }
}
