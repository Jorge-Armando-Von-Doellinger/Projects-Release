namespace OmniSphere.Payments.Core.Exceptions;

public class PaymentFailedException : Exception
{
    public PaymentFailedException() : base("Payment failed")
    {
        
    }

    public PaymentFailedException(string message) : base(message)
    {
        
    }
}