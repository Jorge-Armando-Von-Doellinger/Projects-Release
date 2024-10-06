namespace Nuget.Settings.Messaging
{
    public class RabbitMqSettings
    {
        public string CurrentKey { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Queue { get; set; }
        public string Exchange { get; set; }
        public string TypeExchange { get; set; }
        public string AddKey { get; set; }
        public string DeleteKey { get; set; }
        public string UpdateKey { get; set; }
        public string ResponseKey { get; set; }
    }
}
