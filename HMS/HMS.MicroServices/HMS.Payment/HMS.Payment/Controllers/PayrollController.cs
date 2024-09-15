using HMS.Payments.Application.Interfaces.Manager;
using HMS.Payments.Application.Manager;
using HMS.Payments.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Nuget.Payment.Input;

namespace HMS.Payments.API.Controllers
{
    [ApiController]
    [Route("api/payroll")]
    public class PayrollController : ControllerBase
    {
        private readonly IEmployeePayrollManager _payrollManager;

        public PayrollController(IEmployeePayrollManager payrollManager)
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
        public async Task<IActionResult> AddPayroll(PaymentInput input)
        {
            return Ok(await _payrollManager.AddAsync(input));
        }
    }
}
