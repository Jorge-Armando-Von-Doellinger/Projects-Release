using HMS.ContractsMicroService.API.Attributes;
using HMS.ContractsMicroService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nuget.Contracts.Inputs;

namespace HMS.ContractsMicroService.API.Controllers
{
    [Route("api/WorkHours")]
    [ApiController]
    //[ValidateModel]
    //[HandlerExceptionFilter]
    public class WorkHoursController : ControllerBase
    {
        private readonly IWorkHoursManager _manager;

        public WorkHoursController(IWorkHoursManager manager)
        {
            _manager = manager;
        }
        [HttpGet]
           public async Task<IActionResult> GetWorkHours()
            => Ok(await _manager.GetAll());

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetByID(string ID)
            => Ok(await _manager.GetById(ID));

        [HttpPost]
        public async Task<IActionResult> AddWorkHours(WorkHoursInput input)
        {
            await _manager.Add(input);
            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> Update(WorkHoursUpdateInput input)
        {
            await _manager.Update(input);
            return Accepted();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string ID)
        {
            await _manager.Delete(ID);
            return Accepted();
        }
    }
}
