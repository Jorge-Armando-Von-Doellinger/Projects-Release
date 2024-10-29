namespace HMS.Payments.Messaging.Settings
{
    public sealed class MessagingComponents
    {
        public string AddKey { get; set; }
        public string UpdateKey { get; set; }
        public string DeleteKey { get; set; }
        public string ResponseKey { get; set; }
        public string Exchange { get; set; }
        public string TypeExchange { get; set; }
        public string Queue { get; set; }
        public string[] Keys { get; set; }
        public void SetKeys()
        {
            Keys = new string[4] { AddKey, UpdateKey, DeleteKey, ResponseKey};
        }
    }
}
