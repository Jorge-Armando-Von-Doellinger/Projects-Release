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
        public IActionResult Teste(object obj)
        {
            Console.WriteLine( JsonSerializer.Serialize(_messagingSystem)  + " Controller");
            var component = _messagingSystem.GetPaymentComponent();
            _messagePublisher.PublishSync(obj, component.Exchange, component.Queue, component.AddKey);
            return Accepted();
        }
    }
}
