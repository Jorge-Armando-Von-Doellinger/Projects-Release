using OmniSphere.Payments.Core.Interfaces.Services;

namespace OmniSphere.Payments.Application.Services;

public class PaymentTransactionService : IPaymentTransactionService
{
    public async Task ExecuteAsync(Func<Task> paymentFunction)
    {
        try
        {
            await paymentFunction();
        }
        catch
        {
            await Task.Delay(500); // Simula o rollback do pagamento
        }
    }
}