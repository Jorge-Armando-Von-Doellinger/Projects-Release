using HMS.ContractsMicroService.Messaging.Settings.Interfaces;

namespace HMS.ContractsMicroService.Messaging.Settings
{
    public sealed class MessagingSystem : IMessagingSystem
    {
        public Dictionary<string, IMessagingComponents> Components { get; internal set; }
    }
}
