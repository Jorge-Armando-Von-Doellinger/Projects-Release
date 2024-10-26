using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace HMS.Payments.Application.Interfaces.Manager
{
    public interface IEmployeePaymentManager
    {
        Task<List<PaymentEmployee>> GetAsync();
        Task<List<EmployeePaymentOutput>> GetByEmployeeIdAsync([MaxLength(100)] string employeeID);
        Task<EmployeePaymentOutput> GetByIdAsync(string ID);
        Task AddAsync(EmployeePaymentModel input);
        Task UpdateAsync(EmployeePaymentModel input);
        Task DeleteAsync(Guid ID);
    }
}
