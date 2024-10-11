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
            await _publisher.Publish(input, settings);
            return Accepted();
        }
        [HttpPut]
        public async Task<IActionResult> Update(EmployeeContractUpdateInput input)
        {
            var settings = AppSettings.CurrentSettings.RabbitMq;
            settings.CurrentKey = settings.UpdateKey;
            await _publisher.Publish(input, settings);
            return Accepted();
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Delete(string ID)
        {
            var settings = AppSettings.CurrentSettings.RabbitMq;
            settings.CurrentKey = settings.DeleteKey;
            await _publisher.Publish(ID, settings);
            return Accepted();
        }
    }
}
