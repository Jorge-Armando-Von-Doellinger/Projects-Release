using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly IMessagePublisher _publisher;
        public TesteController(IMessagePublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            await _publisher.Publish("Batata Quente");
            return Accepted();
        }
    }
}
