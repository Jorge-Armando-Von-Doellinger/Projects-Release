using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository
{
    public interface IPaymentRepository // Ela nãó realiza pagamentos, somente cadastra os já realizados!
    {
        Task ExecuteBatchOperations<T>(IEnumerable<T> operations) where T : Payment;
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DeleteAsync(Payment payment);
        Task<Payment> GetByIdAsync(string id);
        Task<List<Payment>> GetAllAsync();
    }
}
