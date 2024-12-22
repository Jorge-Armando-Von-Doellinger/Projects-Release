using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Internal_Services;

public interface IPaymentService
{
    Task<bool> TryProcessPayment(Payment payment);
    Task<bool> TryProcessPayment(PaymentEmployee payment);
    Task TryRefundPaymentAsync(Payment payment);
    Task TryRefundPaymentBatchAsync(IEnumerable<Payment> payment);
}