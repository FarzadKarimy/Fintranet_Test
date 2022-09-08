using Fintranet_Test.Controllers;
using Fintranet_Test.Domain.Entities;
using Fintranet_Test.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fintranet_Test.Test.Test
{
    public class ProductControllerTest
    {

        [Fact]
        public void Index_Test()
        {

            //Arrange

            moqData moqdata = new moqData();

            var moq = new Mock<IProductService>();
              
            moq.Setup(p => p.GetProduct()).Returns(moqdata.GetAll());

            ProductController productController = new ProductController(moq.Object);


            //Act

            var result = productController.Index();

            //Assert

            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);

        }

        [Theory]
        [InlineData(1,-1)]
        public void Details_Test(int ValidId,int inValidId)
        {
            //Arrange
            moqData moqdata = new moqData();
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.GetProduct(ValidId)).Returns(moqdata.GetAll().FirstOrDefault(p=> p.Id == ValidId));
            ProductController productController = new ProductController(moq.Object);
            //Act

            var result = productController.Details(ValidId);

            //Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<Product>(viewResult.ViewData.Model);
            
            //Arrange
            moq.Setup(p => p.GetProduct(inValidId)).Returns(moqdata.GetAll().FirstOrDefault(p => p.Id == inValidId));
            
            //Act
            var invalidResult = productController.Details(inValidId);
            
            //Assert
            Assert.IsType<NotFoundResult>(invalidResult);


        }

        [Fact]
        public void Create_Test()
        {
            //Areange
            var moq = new Mock<IProductService>();

            ProductController controller = new ProductController(moq.Object);

            Product product = new Product()
            {
                Id = 1,
                name = "Samsung",
                price = 4500
            };

            //Act
            var result = controller.Create(product);

            //Assert
            var redirect= Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Null(redirect.ControllerName);
            
            //Areange
            Product invalidProduct = new Product()
            {
                price = 5,
            };

            controller.ModelState.AddModelError("Name", "Please Enter Product Name");

            //Act
            var invalidResult = controller.Create(invalidProduct);

            //Assert
            Assert.IsType<BadRequestObjectResult>(invalidResult);
        }

    }
}
