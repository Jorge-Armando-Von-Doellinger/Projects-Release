using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuget.Clients.DTOs.Mensaging
{
    public class ClientMessagingConfigs
    {
        public ClientMessagingConfigs()
        {
            Exchange = "hms.client";
            Queue = "client";

            BaseRoutingKey = "clients.";
            AddClientKey = BaseRoutingKey + "add";
            DeleteKey = BaseRoutingKey + "delete";
            GetKey = BaseRoutingKey + "get";
            GetByIdKey = BaseRoutingKey + "get_id";
            ResponseKey = BaseRoutingKey + "response";
        }
        public string Exchange { get; set; }
        public string TypeExchange { get; set; }
        public string Queue { get; set; }
        private string BaseRoutingKey { get; set; }
        public string AddClientKey { get; set; }    
        public string UpdateKey { get; set; }
        public string GetKey { get; set; }
        public string DeleteKey { get; set; }
        public string GetByIdKey { get; set; }
        public string ResponseKey { get; set; }
    }
}
