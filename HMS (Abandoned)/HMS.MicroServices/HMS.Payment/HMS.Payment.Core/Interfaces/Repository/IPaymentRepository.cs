using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Repository;

public interface IPaymentRepository // Ela nãó realiza pagamentos, somente cadastra os já realizados!
{
    Task AddManyAsync(List<Payment> payments);
    Task AddManyRefundsAsync(List<Payment> payments);
    Task AddAsync(Payment payment);
    Task AddRefundAsync(Payment payment);
    Task<Payment> GetByIdAsync(string id);
    Task<List<Payment>> GetAllAsync();
}