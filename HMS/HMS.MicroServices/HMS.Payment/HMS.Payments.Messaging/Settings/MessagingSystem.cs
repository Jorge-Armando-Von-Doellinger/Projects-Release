namespace HMS.Payments.Messaging.Settings
{
    public sealed class MessagingSystem
    {
        internal const string PaymentComponent = "PaymentComponent";
        internal const string PaymentEmployeeComponent = "PaymentEmployeeComponent";
        public Dictionary<string, MessagingComponents> MessagingComponents { get; set; }
        public MessagingComponents GetPaymentComponent()
            => MessagingComponents.FirstOrDefault(x => x.Key == PaymentComponent).Value;
        public MessagingComponents GetPaymentEmployeeComponent()
            => MessagingComponents.FirstOrDefault(x => x.Key == PaymentEmployeeComponent).Value;
    }
}
