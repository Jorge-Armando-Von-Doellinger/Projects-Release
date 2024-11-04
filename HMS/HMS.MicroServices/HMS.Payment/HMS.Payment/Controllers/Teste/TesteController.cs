using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HMS.Payments.API.Controllers.Teste
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;
        private readonly MessagingSystem _messagingSystem;
        public TesteController(IMessagePublisher messagePublisher, IOptionsMonitor<MessagingSystem> messagingSystem)
        {
            _messagePublisher = messagePublisher;
            _messagingSystem = messagingSystem.CurrentValue;
        }
        [HttpPost]
        public async Task<IActionResult> Teste(PaymentModel obj)
        {
            var component = _messagingSystem.GetPaymentComponent();
            _messagePublisher.PublishSync(obj, component.Exchange, component.Queue, component.AddKey);
            return Accepted();
        }
        [HttpPut]
        public async Task<IActionResult> Teste1(PaymentUpdateModel obj)
        {
            var component = _messagingSystem.GetPaymentComponent();
            _messagePublisher.PublishSync(obj, component.Exchange, component.Queue, component.UpdateKey);
            return Accepted();
        }
        [HttpPost("2")]
        public async Task<IActionResult> Teste2(PaymentEmployeeModel obj)
        {
            var component = _messagingSystem.GetPaymentEmployeeComponent();
            _messagePublisher.PublishSync(obj, component.Exchange, component.Queue, component.AddKey);
            return Accepted();
        }
        [HttpPut("2")]
        public async Task<IActionResult> Teste3(PaymentEmployeeUpdateModel obj)
        {
            var component = _messagingSystem.GetPaymentComponent();
            _messagePublisher.PublishSync(obj, component.Exchange, component.Queue, component.AddKey);
            return Accepted();
        }
    }
}
