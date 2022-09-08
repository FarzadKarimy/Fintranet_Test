using Fintranet_Test.Domain;
using Fintranet_Test.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Fintranet_Test.Service
{
    public class ProductService : IProductService
    {
        public DataBaseContext _ProductDbContext;
        public ProductService(DataBaseContext ProducteDbContext)
        {
            _ProductDbContext = ProducteDbContext;
        }

        public Product AddProduct(Product product)
        {
            _ProductDbContext.Products.Add(product);
            _ProductDbContext.SaveChanges();
            return product;
        }
        public List<Product> GetProduct()
        {
            return _ProductDbContext.Products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            _ProductDbContext.Products.Update(product);
            _ProductDbContext.SaveChanges();
        }

        public void DeleteProduct(int Id)
        {
            var product = _ProductDbContext.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                _ProductDbContext.Remove(product);
                _ProductDbContext.SaveChanges();
            }
        }

        public Product GetProduct(int Id)
        {
            return _ProductDbContext.Products.FirstOrDefault(x => x.Id == Id);
        }

    }
}
