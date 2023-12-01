using Microsoft.AspNetCore.Mvc;

namespace SS_Microservice.Services.Basket.Controllers
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