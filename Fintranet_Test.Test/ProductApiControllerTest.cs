using Fintranet_Test.Controllers;
using Fintranet_Test.Domain.Entities;
using Fintranet_Test.Service;
using Fintranet_Test.Test.Test;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fintranet_Test.Test
{
    public class ProductApiControllerTest
    {
        moqData _moqdata;
        public ProductApiControllerTest()
        {
            _moqdata = new moqData();
        }


        [Fact]
        public void GetTest()
        {
            //Arrange
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.GetProduct()).Returns(_moqdata.GetAll());
            ProductApiController apiController = new ProductApiController(moq.Object);

            //Act
            var result = apiController.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            var list = result as OkObjectResult;
            Assert.IsType<List<Product>>(list.Value);
        }

        [Theory]
        [InlineData(1, -1)]
        public void GetByIdTest(int ValidId, int inValidId)
        {
            //Arrange
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.GetProduct(ValidId)).Returns(_moqdata.GetAll().FirstOrDefault(p => p.Id == ValidId));
            ProductApiController apiController = new ProductApiController(moq.Object);

            //Act
            var result = apiController.Get(ValidId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            var product = result as OkObjectResult;
            Assert.IsType<Product>(product.Value);

            //--------------------------

            //Arrange
            moq.Setup(p => p.GetProduct(inValidId)).Returns(_moqdata.GetAll().FirstOrDefault(p => p.Id == inValidId));

            //Act
            var invalidResult = apiController.Get(inValidId);

            //Assert
            Assert.IsType<NotFoundResult>(invalidResult);

        }

        [Fact]
        public void Post_Test()
        {
            //Arrange
            var moq = new Mock<IProductService>();

            ProductApiController controller = new ProductApiController(moq.Object);

            Product product = new Product()
            {
                name = "Samsung",
                price = 4500
            };

            //Act
            var result = controller.Post(product);

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }



        [Theory]
        [InlineData(1, -1)]
        public void Delete_Test(int ValidId, int inValidId)
        {
            //Arrange
            var moq = new Mock<IProductService>();

            moq.Setup(p => p.DeleteProduct(ValidId));
            moq.Setup(p => p.GetProduct(ValidId)).Returns(_moqdata.GetAll().FirstOrDefault(p => p.Id == ValidId));

            ProductApiController apiController = new ProductApiController(moq.Object);

            //Act
            var result = apiController.Delete(ValidId);
            var invalidResult = apiController.Delete(inValidId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<NotFoundResult>(invalidResult);
        }
    }
}
