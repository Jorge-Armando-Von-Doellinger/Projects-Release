using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository
{
    public interface IEmployeePaymentRepository
    {
        Task AddAsync(PaymentEmployee payroll);
        Task UpdateAsync(PaymentEmployee payroll);
        Task DeleteAsync(string payrollID);
        Task<PaymentEmployee> GetByID(string ID);
        Task<List<PaymentEmployee>> GetAllAsync();
        Task<List<PaymentEmployee>> GetByEmployeeID(Guid EmployeeID);
    }
}
