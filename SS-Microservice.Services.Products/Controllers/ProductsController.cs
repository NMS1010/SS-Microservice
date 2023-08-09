using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public IActionResult GetProducts()
        {
            return Ok("Product list");
        }

        [HttpPost("create")]
        public IActionResult AddProduct()
        {
            return Ok("Creating product");
        }
    }
}