using OmniSphere.Payments.Core.Entity;

namespace OmniSphere.Payments.Core.Interfaces.Services;

public interface IPaymentService
{
    Task ExecutePaymentAsync(Payment payment);
    
}