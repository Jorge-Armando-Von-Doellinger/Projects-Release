using HMS.ContractsMicroService.API.Services;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nuget.Settings;
using Nuget.Settings.ServiceDiscovery;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IDiscoveryService _serviceDiscovery;
        private readonly IHostEnvironment _environment;
        private IMemoryCache _cache;

        public SettingsController(IDiscoveryService serviceDiscovery, IHostEnvironment environment, IMemoryCache memory) 
        {
            _serviceDiscovery = serviceDiscovery;
            _environment = environment;
            _cache = memory;
        }

        [HttpPost]
        public async Task<IActionResult> AddSettings(IAppSettings consulSettings)
        {
            await _serviceDiscovery.Put(consulSettings);
            return Accepted();
        }

        [HttpGet]
        public IActionResult GetSettingsCache()
        {
            return Ok(_cache.Get("settings"));
        }
    }
}
