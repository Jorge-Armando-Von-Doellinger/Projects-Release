using Nuget.Settings.Messaging;

namespace HMS.ContractsMicroService.API.Settings.Messaging
{
    public class MessagingSystem : IMessagingSystem
    {
        public string Queue { get; protected set; }

        public string Exchange { get; protected set; }

        public string TypeExchange { get; protected set; }

        public string AddKey { get; protected set; }

        public string DeleteKey { get; protected set; }

        public string UpdateKey { get; protected set; }

        public string ResponseKey { get; protected set; }

        public string CurrentKey { get; protected set; }

        public string[] GetKeys()
        {
            return new string[4]
            {
                AddKey, UpdateKey, DeleteKey, ResponseKey
            };
        }
    }
}
