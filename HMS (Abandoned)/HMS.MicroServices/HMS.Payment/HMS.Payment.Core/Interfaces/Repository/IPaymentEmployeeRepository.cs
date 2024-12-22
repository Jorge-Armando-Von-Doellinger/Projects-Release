using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository;

public interface IPaymentEmployeeRepository
{
    Task AddManyAsync(List<PaymentEmployee> employees);
    Task AddAsync(PaymentEmployee payment);
    Task<PaymentEmployee> GetByIdAsync(string id);
    Task<List<PaymentEmployee>> GetAllAsync();
    Task<List<PaymentEmployee>> GetByEmployeeId(string employeeId);
}