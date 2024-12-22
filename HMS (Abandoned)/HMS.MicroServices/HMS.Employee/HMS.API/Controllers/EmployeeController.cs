using HMS.Employee.Application.Validator;
using HMS.Employee.Core.Interface.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuget.Employee.Inputs;
using Nuget.Employee.Inputs.Update;

namespace HMS.Employee.API.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IManager<Nuget.Response.Response, EmployeeInput> _manager;
        public EmployeeController(IManager<Nuget.Response.Response, EmployeeInput> manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
            => Ok(await _manager.Get());
        [HttpGet("ID")]
        public async Task<IActionResult> GetById(Guid ID)
        {
            return Ok(await _manager.GetById(ID));
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeInput input)
        {
            var validate = await BasicValidator.Validate(input);
            if (validate == null)
                return Ok(await _manager.Add(input));
            return BadRequest(validate);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdate input)
        {
            var validate = await BasicValidator.Validate(input);
            if (validate == null)
                return Ok(await _manager.Update(input));
            return BadRequest(validate);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(Guid ID) // Ou demitir
            => Ok(await _manager.DeleteById(ID));
    }
}
