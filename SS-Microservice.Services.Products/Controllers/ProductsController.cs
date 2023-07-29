using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
        }

        [HttpGet("all")]
        public IActionResult GetProducts()
        {
            return Ok("Product list");
        }
    }
}