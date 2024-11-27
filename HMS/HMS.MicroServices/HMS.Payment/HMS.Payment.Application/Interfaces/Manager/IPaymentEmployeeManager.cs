using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;
using HMS.Payments.Application.Models.Update;

namespace HMS.Payments.Application.Interfaces.Manager;

public interface IPaymentEmployeeManager
{
    Task<List<PaymentEmployeeOutput>> GetAllAsync();
    Task<List<PaymentEmployeeOutput>> GetByEmployeeIdAsync(string employeeID);
    Task<PaymentEmployeeOutput> GetByIdAsync(string ID);
    Task AddAsync(PaymentEmployeeModel input);
    Task UpdateAsync(PaymentEmployeeUpdateModel input);
    Task DeleteAsync(string ID);
}