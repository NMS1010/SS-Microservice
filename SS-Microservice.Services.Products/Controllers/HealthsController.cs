using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthsController : ControllerBase
    {
        [HttpGet("status")]
        public IActionResult CheckHealth()
        {
            return Ok();
        }
    }
}