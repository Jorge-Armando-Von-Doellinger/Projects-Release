using HMS.ContractsMicroService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        private readonly IHostEnvironment _environment;


        public SettingsController(IServiceDiscovery serviceDiscovery, IHostEnvironment environment)
        {
            _serviceDiscovery = serviceDiscovery;
            _environment = environment;
        }

        /*[HttpPost]
        public async Task<IActionResult> AddSettings(IAppSettings consulSettings)
        {
            await _serviceDiscovery.Put(consulSettings);
            return Accepted();
        }*/

        /*[HttpGet]
        public IActionResult GetSettingsCache()
        {
            return Ok(_cache.Get("settings"));
        }*/
    }
}
