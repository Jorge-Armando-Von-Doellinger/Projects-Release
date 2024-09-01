using HMS.Employee.Application.Manager;
using HMS.Employee.Application.Validator;
using HMS.Employee.Core.Entity;
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
        private readonly IManagerWithEmployeeId<Response, PayrollInput> _manager;
        public PayrollControler(IManagerWithEmployeeId<Response, PayrollInput> manager)
        {
            _manager = manager;
        }

        [HttpGet("EmployeeId")]
        public async Task<IActionResult> GetByEmployeeId(Guid ID)
        {
            return Ok(await _manager.GetByEmployeeId(ID));
        }

        [HttpPost]
        public async Task<IActionResult> AddPàyroll(PayrollInput input)
        {
            var validate = await BasicValidator.Validate(input);
            return Ok(await _manager.Add(input));
        }
    }
}
