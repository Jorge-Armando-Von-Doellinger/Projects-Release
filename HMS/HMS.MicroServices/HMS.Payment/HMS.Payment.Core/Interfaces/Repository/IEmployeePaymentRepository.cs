using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository
{
    public interface IEmployeePaymentRepository
    {
        Task AddAsync(PaymentEmployee payment);
        Task UpdateAsync(PaymentEmployee payment);
        Task DeleteAsync(string paymentID);
        Task<PaymentEmployee> GetByIDAsync(string ID);
        Task<List<PaymentEmployee>> GetAllAsync();
        Task<List<PaymentEmployee>> GetByEmployeeID(string EmployeeID);
    }
}
