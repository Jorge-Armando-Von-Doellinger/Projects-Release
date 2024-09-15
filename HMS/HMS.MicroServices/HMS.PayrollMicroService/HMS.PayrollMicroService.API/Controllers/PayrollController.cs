using HMS.PayrollMicroService.Application.Interfaces.Manager;
using HMS.PayrollMicroService.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Nuget.Payroll.Input;

namespace HMS.PayrollMicroService.API.Controllers
{
    [ApiController]
    [Route("api/payroll")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollManager _payrollManager;

        public PayrollController(IPayrollManager payrollManager)
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
        public async Task<IActionResult> AddPayroll(PayrollInput input)
        {
            return Ok(await _payrollManager.AddAsync(input));
        }
    }
}
