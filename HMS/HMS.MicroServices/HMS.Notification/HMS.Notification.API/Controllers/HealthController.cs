using Microsoft.AspNetCore.Mvc;

namespace HMS.Notification.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class HealthController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Service is up");
    }
}