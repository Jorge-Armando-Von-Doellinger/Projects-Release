using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository
{
    public interface IPaymentEmployeeRepository
    {
        Task AddAsync(PaymentEmployee payment);
        Task AddBatchAsync(IEnumerable<PaymentEmployee> payments);
        Task UpdateAsync(PaymentEmployee payment);
        Task UpdateBatchAsync(IEnumerable<PaymentEmployee> payments);
        Task DeleteAsync(string paymentID);
        Task DeleteBatchAsync(IEnumerable<string> paymentID);
        Task<PaymentEmployee> GetByIDAsync(string ID);
        Task<List<PaymentEmployee>> GetAllAsync();
        Task<List<PaymentEmployee>> GetByEmployeeID(string EmployeeID);
    }
}
