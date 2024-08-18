namespace Nuget.Clients.DTOs.Mensaging
{
    public class ClientMessagingConfigs
    {
        public ClientMessagingConfigs()
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
        public string TypeExchange { get;}
        public string Queue { get;}
        private string BaseRoutingKey { get;}
        public string AddKey { get; }    
        public string UpdateKey { get; }
        public string GetKey { get; }
        public string DeleteKey { get; }
        public string GetByIdKey { get; }
        public string ResponseBase { get; }
    }
}
