using HMS.ContractsMicroService.API.Attributes;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/Contract")]
    [ApiController]
    [ValidateModel]
    //[HandlerExceptionFilter]
    public class ContractController : ControllerBase
    {
        private readonly IContractManager _manager;

        public ContractController(IContractManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetContracts() => Ok(await _manager.GetAll());
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetContractById(string ID) => Ok(await _manager.GetById(ID));
        [HttpPost]
        public async Task<IActionResult> AddContract( contract)
        {
            await _manager.Add(contract);
            return Accepted();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateContract(ContractUpdateInput contract)
        {
            await _manager.Update(contract);
            return Accepted();
        }
        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteContract(string ID)
        {
            await _manager.Delete(ID);
            return Accepted();
        }
    }
}
