using HMS.PayrollMicroService.Core.Entity;
using Nuget.Payroll.Input;

namespace HMS.PayrollMicroService.Application.Interfaces.Manager
{
    public interface IPayrollManager
    {
        Task<List<Payroll>> GetAsync();
        Task<List<Payroll>> GetByEmployeeIdAsync(Guid employeeID);
        Task<Payroll> GetByIdAsync(Guid ID);
        Task<bool> AddAsync(PayrollInput input);
        Task UpdateAsync(PayrollInput input);
        Task DeleteAsync(Guid ID);
    }
}
