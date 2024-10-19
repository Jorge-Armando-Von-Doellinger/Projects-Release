using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Core.Interfaces.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace HMS.ContractsMicroService.API.Controllers
{
    
    [Route("api/Teste")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly IMessagePublisher _publisher;

        public TesteController(IMessagePublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public async Task<IActionResult> AddContract(ContractInput input)
        {
            await _publisher.Publish(input, "contract.add");
            return Accepted();
        }
        [HttpPut]
        public async Task<IActionResult> AddContract(ContractUpdateInput input)
        {
            await _publisher.Publish(input, "contract.update");
            return Accepted();
        }
        [HttpDelete]
        public async Task<IActionResult> AddContract(ContractDeleteInput input)
        {
            await _publisher.Publish(input, "ontract.remove");
            return Accepted();
        }
    }
}
