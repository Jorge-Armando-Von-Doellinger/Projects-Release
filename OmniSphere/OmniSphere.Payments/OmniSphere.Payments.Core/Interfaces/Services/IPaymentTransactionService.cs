namespace OmniSphere.Payments.Core.Interfaces.Services;

public interface IPaymentTransactionService
{
    Task ExecuteAsync(Func<Task> paymentFunction);
}