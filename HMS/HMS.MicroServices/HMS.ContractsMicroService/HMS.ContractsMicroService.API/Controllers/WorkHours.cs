using HMS.ContractsMicroService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuget.Contracts.Inputs;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/WorkHours")]
    [ApiController]
    public class WorkHours : ControllerBase
    {
        private readonly IWorkHoursManager _manager;

        public WorkHours(IWorkHoursManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
        public async Task<IActionResult> GetWorkHours()
            => Ok(await _manager.GetAll());

        [HttpPost]
        public async Task<IActionResult> AddWorkHours(WorkHoursInput input)
        {
            await _manager.Add(input);
            return Accepted();
        }
    }
}
