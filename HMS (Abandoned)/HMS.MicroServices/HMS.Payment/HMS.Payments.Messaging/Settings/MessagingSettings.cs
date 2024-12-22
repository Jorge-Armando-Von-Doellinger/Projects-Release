namespace HMS.Payments.Messaging.Settings;

public class MessagingSettings
{
    public string Address { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public Dictionary<string, string> Queues { get; set; }
    public string Exchange { get; set; }
    public string TypeExchange { get; set; }

    public string GetPaymentQueue()
    {
        return Queues?.FirstOrDefault(x => x.Key == "Payment").Value;
    }

    public string GetPaymentEmployeeQueue()
    {
        return Queues?.FirstOrDefault(x => x.Key == "PaymentEmployee").Value;
    }

    public string GetRefundQueue()
    {
        return Queues?.FirstOrDefault(x => x.Key == "Refunds").Value;
    }

    internal string GetRetryQueue()
    {
        return Queues?.FirstOrDefault(x => x.Key == "Retry-AllPayments").Value;
    }

    internal string GetUnprocessableQueue()
    {
        return Queues?.FirstOrDefault(x => x.Key == "Unprocessable-payments").Value;
    }
}