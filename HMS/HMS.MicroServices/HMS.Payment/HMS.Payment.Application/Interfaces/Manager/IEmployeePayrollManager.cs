using HMS.Payments.Core.Entity;
using Nuget.Payment.Input;

namespace HMS.Payments.Application.Interfaces.Manager
{
    public interface IEmployeePayrollManager
    {
        Task<List<EmployeePayment>> GetAsync();
        Task<List<EmployeePayment>> GetByEmployeeIdAsync(Guid employeeID);
        Task<EmployeePayment> GetByIdAsync(Guid ID);
        Task<bool> AddAsync(PaymentInput input);
        Task UpdateAsync(PaymentInput input);
        Task DeleteAsync(Guid ID);
    }
}
