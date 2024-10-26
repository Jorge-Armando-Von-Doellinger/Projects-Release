using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Payments.API.Controllers
{
    [ApiController]
    [Route("api/payroll")]
    public class PayrollController : ControllerBase
    {
        private readonly IEmployeePaymentManager _payrollManager;

        public PayrollController(IEmployeePaymentManager payrollManager)
        {
            _payrollManager = payrollManager;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _payrollManager.GetAsync());
        }

        [HttpGet("benefits")]
        public async Task<IActionResult> GetBenefits()
        {
            var benefits = Enum.GetValues(typeof(BenefitsEnum))
                .Cast<BenefitsEnum>()
                .Select(b => b.ToString())
                .ToList();
            return Ok(benefits);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayroll(EmployeePaymentModel input)
        {
            await _payrollManager.AddAsync(input);
            return Accepted();
        }
    }
}
