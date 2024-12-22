using HMS.ContractsMicroService.API.Attributes;
using HMS.ContractsMicroService.Application.DTOs.Input;
using HMS.ContractsMicroService.Application.DTOs.UpdateInput;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/Contract/Employee")]
    [ApiController]
    [ValidateModel]
    [HandlerExceptionFilter]
    public class EmployeeContractController : ControllerBase
    {
        private readonly IEmployeeContractManager _manager;

        public EmployeeContractController(IEmployeeContractManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeContracts()
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
