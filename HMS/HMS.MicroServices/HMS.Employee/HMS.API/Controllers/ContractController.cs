using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Enum;
using HMS.Employee.Core.Interface.Manager;
using Microsoft.AspNetCore.Mvc;
using Nuget.Employee.Inputs;
using Nuget.Response;

namespace HMS.Employee.API.Controllers
{
    [ApiController]
    [Route("api/contract")]
    public class ContractController : ControllerBase
    {
        private readonly IManagerWithEmployeeId<Response, ContractualInformationInput<BenefitsEnum>> _manager;
        public ContractController(IManagerWithEmployeeId<Response, ContractualInformationInput<BenefitsEnum>> manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _manager.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContractualInformationInput<BenefitsEnum> input)
        {
            return Ok(await _manager.Add(input));
        }
    }
}
