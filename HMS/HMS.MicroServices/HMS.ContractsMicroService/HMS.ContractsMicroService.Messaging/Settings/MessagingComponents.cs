using HMS.ContractsMicroService.Messaging.Settings.Interfaces;

namespace HMS.ContractsMicroService.Messaging.Settings
{
    public sealed class MessagingComponents : IMessagingComponents
    {
        public string Queue { get; set; }

        public string Exchange { get; set; }

        public string TypeExchange { get; set; }

        public string AddKey { get; set; }

        public string DeleteKey { get; set; }

        public string UpdateKey { get; set; }

        public string ResponseKey { get; set; }

        public string CurrentKey { get; set; }
    }
}
