using Nuget.Clients.DTOs.Mensaging;
using RabbitMQ.Client;

namespace HMS.Messaging.Configurators
{
    public class ClientChannelConfigurator
    {
        public ClientChannelConfigurator() 
        { 
        
        }

        public void SetConfigs(IModel channel)
        {
            var configs = new ClientMessagingConfigs();
            channel.ExchangeDeclare(configs.Exchange, 
                configs.TypeExchange,
                false, 
                false, 
                null);
            channel.QueueDeclare(configs.Queue,
                false,
                false,
                false,
                null);
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.AddClientKey); 
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.UpdateKey); 
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.DeleteKey);
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.GetKey); 
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.GetByIdKey); 
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.ResponseKey);
            
        }
    }
}
