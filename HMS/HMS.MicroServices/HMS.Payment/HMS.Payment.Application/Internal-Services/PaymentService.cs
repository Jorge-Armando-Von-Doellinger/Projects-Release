using HMS.Payments.Core.Entity;
using HMS.Payments.Core.Enums;
using HMS.Payments.Core.Interfaces.Internal_Services;

namespace HMS.Payments.Application.Internal_Services;

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
            payment.Status = PaymentStatus.Pending;
            await Task.Delay(100); // Simulando pagamento
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}