namespace HMS.Payments.Messaging.Settings
{
    public class MessagingSettings
    {
        public string Address { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public Dictionary<string, string> Queues { get; set; }
        public string Exchange { get; set; }
        public string TypeExchange { get; set; }
        
        internal string GetPaymentQueue() => Queues?.FirstOrDefault(x => x.Key == "Payment").Value;
        internal string GetPaymentEmployeeQueue() => Queues?.FirstOrDefault(x => x.Key == "PaymentEmployee").Value;
        internal string GetRetryQueue() => Queues?.FirstOrDefault(x => x.Key == "Retry-AllPayments").Value;
        internal string GetUnprocessableQueue() => Queues?.FirstOrDefault(x => x.Key == "Retry-AllPayments").Value;
    }
}
