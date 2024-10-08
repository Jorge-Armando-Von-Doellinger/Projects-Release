using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuget.Contracts.Inputs;
using Nuget.Settings;
using Nuget.Settings.Messaging;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly IMessagePublisher<RabbitMqSettings> _publisher;
        public TesteController(IMessagePublisher<RabbitMqSettings> publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeContractInput input)
        {
            var settings = AppSettings.CurrentSettings.RabbitMq;
            settings.CurrentKey = settings.AddKey;
            Console.WriteLine(settings.CurrentKey);
            await _publisher.Publish(input, settings);
            return Accepted();
        }
    }
}
