using HMS.Employee.Application.Manager;
using HMS.Employee.Application.Validator;
using HMS.Employee.Core.Data.Discounts;
using HMS.Employee.Core.Entity;
using HMS.Employee.Core.Enum;
using HMS.Employee.Core.Interface.Manager;
using Microsoft.AspNetCore.Mvc;
using Nuget.Employee.Inputs;
using Nuget.Response;

namespace HMS.Employee.API.Controllers
{
    [Route("api/Payroll")]
    [ApiController]
    public sealed class PayrollControler : ControllerBase
    {
        private readonly IManagerWithEmployeeId<Response, PayrollInput<BenefitsEnum, Discount>> _manager;
        public PayrollControler(IManagerWithEmployeeId<Response, PayrollInput<BenefitsEnum, Discount>> manager)
        {
            _manager = manager;
        }

        [HttpGet]   
        public async Task<IActionResult> Get()
        {
            return Ok(await _manager.Get());
        }

        [HttpGet("EmployeeId")]
        public async Task<IActionResult> GetByEmployeeId(Guid ID)
        {
            return Ok(await _manager.GetByEmployeeId(ID));
        }

        [HttpPost]
        public async Task<IActionResult> AddPàyroll(PayrollInput<BenefitsEnum, Discount> input)
        {
            var validate = await BasicValidator.Validate(input);
            return Ok(await _manager.Add(input));
        }
    }
}
