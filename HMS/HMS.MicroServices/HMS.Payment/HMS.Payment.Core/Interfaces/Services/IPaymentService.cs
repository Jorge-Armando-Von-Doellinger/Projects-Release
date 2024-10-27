using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<bool> TryProcessPayment(Payment payment);
        Task<bool> TryProcessPayment(PaymentEmployee payment);
    }
}
