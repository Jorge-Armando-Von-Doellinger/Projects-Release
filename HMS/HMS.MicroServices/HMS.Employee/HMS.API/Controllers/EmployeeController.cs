using HMS.Employee.Core.Interface.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuget.Employee.Inputs;

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
        public async Task<IActionResult> GetById(long ID)
            => Ok();
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeInput input)
            => Ok(_manager.Add(input));
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee()
            => Ok();
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee() // Ou demitir
            => Ok();
    }
}
