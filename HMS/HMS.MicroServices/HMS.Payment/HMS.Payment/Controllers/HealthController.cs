using Microsoft.AspNetCore.Mvc;

namespace HMS.Payments.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Service is health");
        }
    }
}
