using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Update;
using HMS.Payments.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Payments.API.Controllers
{
    [ApiController]
    [Route("api/Payroll")]
    [ProducesResponseType(StatusCodes.Status200OK)] // Resposta bem-sucedida
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Recurso não encontrado
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Solicitação inválida
    public class PayrollController : ControllerBase
    {
        private IEmployeePaymentManager _employeePaymentManager;
        public PayrollController(IEmployeePaymentManager employeePayrollManager)
        {
            _employeePaymentManager = employeePayrollManager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _employeePaymentManager.GetAsync());
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> Get(string ID)
        {
            // Authenticação OBVIAMENTE
            return Ok(await _employeePaymentManager.GetByIdAsync(ID));
        }

        [HttpGet("benefits")]
        public IActionResult GetBenefits()
        {
            var benefits = Enum.GetValues(typeof(BenefitsEnum))
                .Cast<BenefitsEnum>()
                .Select(b => b.ToString())
                .ToList();
            return Ok(benefits);
        }
        [HttpPost]

        public async Task<IActionResult> AddPayroll(PaymentEmployeeModel input)
        {
            await _employeePaymentManager.AddAsync(input);
            return Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePayroll(PaymentEmployeeUpdateModel model)
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
