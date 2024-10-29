using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Payments.API.Controllers
{
    [Route("api/v1/Payment/Employee")]
    [ApiController]
    public class PaymentEmployeeController : ControllerBase
    {
        private readonly IPaymentEmployeeManager _manager;
        public PaymentEmployeeController(IPaymentEmployeeManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _manager.GetAllAsync());
        }
        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(string ID)
        {
            // Authenticação OBVIAMENTE
            return Ok(await _manager.GetByIdAsync(ID));
        }

        [HttpPost]

        public async Task<IActionResult> AddPayroll(PaymentEmployeeModel input)
        {
            await _manager.AddAsync(input);
            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayroll(PaymentEmployeeUpdateModel model)
        {
            await _manager.UpdateAsync(model);
            return Accepted();
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePayroll()
        {
            return BadRequest("Not Implemented");
        }

    }
}
