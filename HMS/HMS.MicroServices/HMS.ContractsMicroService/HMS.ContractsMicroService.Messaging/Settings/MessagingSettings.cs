using HMS.ContractsMicroService.Messaging.Settings.Interfaces;

namespace HMS.ContractsMicroService.Messaging.Settings
{
    public sealed class MessagingSettings : IMessagingSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
    }
}
