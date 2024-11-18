namespace HMS.Payments.Messaging.Settings
{
    public class MessagingSettings
    {
        public string Address { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public List<string> Queues { get; set; }
        public string Exchange { get; set; }
        public string TypeExchange { get; set; }
    }
}
