using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Enums;
using HMS.Payments.Core.Interfaces.Internal_Services;

namespace HMS.Payments.Application.Services;

public sealed class PaymentService : IPaymentService
{
    public async Task<bool> TryProcessPayment(Payment payment)
    {
        try
        {
            payment.ValidateEntity();
            payment.Status = PaymentStatus.Pending;
            await Task.Delay(100); // Simulando pagamento
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> TryProcessPayment(PaymentEmployee payment)
    {
        try
        {
            payment.ValidateEntity();
            await Task.Delay(100); // Simulando pagamento
            payment.Status = PaymentStatus.Completed;
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task TryRefundPaymentAsync(Payment payment)
    {
        try
        {
            payment.ValidateEntity();
            await Task.Delay(100);
            payment.Status = PaymentStatus.Refunded;
        }
        catch
        {
            payment.Status = PaymentStatus.Failed;
        }
    }

    public async Task TryRefundPaymentBatchAsync(IEnumerable<Payment> payments)
    {
        foreach (var payment in payments)
            try
            {
                payment.ValidateEntity();
                await Task.Delay(150);
                payment.Status = PaymentStatus.Refunded;
            }
            catch
            {
                payment.Status = PaymentStatus.Failed;
            }
    }
}