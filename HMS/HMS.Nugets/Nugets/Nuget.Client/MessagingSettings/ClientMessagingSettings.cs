using Nuget.MessagingUtilities.MessageSettings;

namespace Nuget.Client.MessagingSettings
{
    public sealed class ClientMessagingSettings : IMessageSettings
    {
        public ClientMessagingSettings()
        {
            Exchange = "hms.client";
            TypeExchange = "direct";
            Queue = "client";

            BaseRoutingKey = "clients";

            AddKey = BaseRoutingKey + ".add";
            UpdateKey = BaseRoutingKey + ".update";
            DeleteKey = BaseRoutingKey + ".delete";
            GetKey = BaseRoutingKey + ".get";
            GetByIdKey = BaseRoutingKey + ".get_id";

            ResponseBase = BaseRoutingKey;
        }
        public string Exchange { get; }
        public string TypeExchange { get; }
        public string Queue { get; }
        public string BaseRoutingKey { get; }
        public string AddKey { get; }
        public string UpdateKey { get; }
        public string? GetKey { get; }
        public string DeleteKey { get; }
        public string? GetByIdKey { get; }
        public string ResponseBase { get; }
        public string CurrentKey { get; set; }
    }
}
