using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Payments.API.Controllers
{
    [ApiController]
    [Route("api/v1/Payment")]
    /*    [ProducesResponseType(StatusCodes.Status200OK)] // Resposta bem-sucedida
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Recurso não encontrado
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Solicitação inválida*/
    public class PaymentController : ControllerBase
    {
        private IPaymentManager _employeePaymentManager;
        public PaymentController(IPaymentManager employeePayrollManager)
        {
            _employeePaymentManager = employeePayrollManager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeePaymentManager.GetAllAsync());
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(string ID)
        {
            // Authenticação OBVIAMENTE
            return Ok(await _employeePaymentManager.GetByIdAsync(ID));
        }

        [HttpPost]

        public async Task<IActionResult> AddPayroll(PaymentModel input)
        {
            await _employeePaymentManager.AddAsync(input);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayroll(PaymentUpdateModel model)
        {
            await _employeePaymentManager.UpdateAsync(model);
            return Accepted();
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePayroll()
        {
            return BadRequest("Not Implemented");
        }
    }
}
