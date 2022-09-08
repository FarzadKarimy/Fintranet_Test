using Fintranet_Test.Domain.Entities;
using Fintranet_Test.Service;
using Microsoft.AspNetCore.Mvc;

namespace Fintranet_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductApiController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetProduct());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _service.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok( product);
        }

        [HttpPost]

        public IActionResult Post([FromBody]Product value)
        {
            return Ok(_service.AddProduct(value));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _service.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            _service.DeleteProduct(id);
            return Ok(true);
        }
    }
}
