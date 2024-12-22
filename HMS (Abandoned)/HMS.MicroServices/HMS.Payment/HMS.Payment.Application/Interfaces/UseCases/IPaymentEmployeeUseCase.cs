using HMS.Payments.Application.Models.Input;
using HMS.Payments.Application.Models.Output;

namespace HMS.Payments.Application.Interfaces.UseCases;

public interface IPaymentEmployeeUseCase
{
    Task<List<PaymentEmployeeOutput>> GetAllAsync();
    Task<List<PaymentEmployeeOutput>> GetByEmployeeIdAsync(string employeeId);
    Task<PaymentEmployeeOutput> GetByIdAsync(string id);
    Task AddAsync(PaymentEmployeeModel input);
    Task AddManyAsync(List<PaymentEmployeeModel> input);
}