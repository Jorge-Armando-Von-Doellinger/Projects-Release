using OmniSphere.Payments.Core.Entity;

namespace OmniSphere.Payments.Core.Interfaces.Repository;

public interface IPaymentRepository
{
    Task RegisterAsync(Payment payment);
    Task DeleteAsync(Payment payment);
    Task<Payment> GetByIdAsync(string paymentId);
    Task<List<Payment>> GetByAccountIdAsync(string accountId);
}