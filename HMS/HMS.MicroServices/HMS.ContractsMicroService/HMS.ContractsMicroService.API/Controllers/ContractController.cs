using HMS.ContractsMicroService.API.Attributes;
using HMS.ContractsMicroService.Application.Interfaces;
using HMS.ContractsMicroService.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nuget.Contracts.Inputs;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ValidateModel]
    //[HandlerExceptionFilter]
    public class ContractController : ControllerBase
    {
        private readonly IEmployeeContractManager _manager;
        private readonly IMemoryCache _cache;

        public ContractController(IEmployeeContractManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetContracts()
            => Ok(await _manager.GetAll());
         
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetContractById(string ID)
        {
            return Ok(await _manager.GetById(ID));
        }

        [HttpPost]
        public async Task<IActionResult> AddContract(EmployeeContractInput contract)
        {
            await _manager.Add(contract);
            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContract(EmployeeContractUpdateInput input)
        {
            await _manager.Update(input);
            return Accepted();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContract(string ID)
        {
            await _manager.Delete(ID);
            return Accepted();
        }
            
    }
}
