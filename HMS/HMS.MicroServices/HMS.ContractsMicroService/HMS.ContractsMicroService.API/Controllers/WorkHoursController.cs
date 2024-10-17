using HMS.ContractsMicroService.API.Attributes;
using HMS.ContractsMicroService.Application.Interfaces.Managers;
using HMS.ContractsMicroService.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddWorkHours(WorkHours input)
        {
            await _manager.Add(input);
            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> Update(WorkHours input)
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
