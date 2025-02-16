using OmniSphere.Payments.Core.Entity;
using OmniSphere.Payments.Core.Interfaces.Services;

namespace OmniSphere.Payments.Application.Services;

public class PaymentService : IPaymentService
{
    public PaymentService()
    {
        
    }


    public async Task ExecutePaymentAsync(Payment payment)
    {
        await Task.Delay(1000); // Simulando pagamento
    }
}