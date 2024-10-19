using HMS.ContractsMicroService.Messaging.Settings.Interfaces;

namespace HMS.ContractsMicroService.Messaging.Settings
{
    public sealed class MessagingComponents : IMessagingComponents
    {
        public MessagingComponents()
        {
        }
        public string Queue { get; set; }

        public string Exchange { get; set; }

        public string TypeExchange { get; set; }

        public string AddKey { get; set; }

        public string DeleteKey { get; set; }

        public string UpdateKey { get; set; }

        public string ResponseKey { get; set; }

        public string CurrentKey { get; set; }

        public string[] Keys { get; private set; } = new string[4];

            
        public void SetKeys()
        {
            Keys = new string[4] { AddKey, UpdateKey, DeleteKey, UpdateKey };
        }
    }
}
