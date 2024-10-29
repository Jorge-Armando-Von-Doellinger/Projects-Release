using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Enums;
using HMS.Payments.Core.Interfaces.Services;

namespace HMS.Payments.Application.Services
{
    public sealed class PaymentService : IPaymentService
    {
        public Task<bool> TryProcessPayment(Payment payment)
        {
            try
            {
                payment.ValidateEntity();
                payment.Status = PaymentStatus.Pending;
                Thread.Sleep(500); // Simular o pagamento
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);

            }
        }

        public Task<bool> TryProcessPayment(PaymentEmployee payment)
        {
            try
            {
                payment.ValidateEntity();
                payment.Status = PaymentStatus.Pending;
                Thread.Sleep(500); // Simulando um pagamento!
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);

            }
        }
    }
}
