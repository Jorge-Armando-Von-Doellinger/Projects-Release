using HMS.ContractsMicroService.Core.Interfaces.Settings;
using Microsoft.AspNetCore.Mvc;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IDiscoveryService _serviceDiscovery;
        private readonly IHostEnvironment _environment;


        public SettingsController(IDiscoveryService serviceDiscovery, IHostEnvironment environment)
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
