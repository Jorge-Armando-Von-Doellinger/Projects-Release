using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using System.ComponentModel.DataAnnotations;

namespace HMS.Payments.Application.Interfaces.Manager
{
    public interface IEmployeePaymentManager
    {
        Task<List<EmployeePaymentOutput>> GetAsync();
        Task<List<EmployeePaymentOutput>> GetByEmployeeIdAsync([MaxLength(100)]string employeeID);
        Task<EmployeePaymentOutput> GetByIdAsync(string ID);
        Task AddAsync(EmployeePaymentModel input);
        Task UpdateAsync(EmployeePaymentModel input);
        Task DeleteAsync(Guid ID);
    }
}
