using Nuget.Client.MessagingSettings;
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
            var configs = new ClientMessagingSettings();
            channel.ExchangeDeclare(configs.Exchange, 
                ExchangeType.Direct,
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
                configs.AddKey); 
            /*channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.UpdateKey); */
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.DeleteKey);
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.GetKey); 
            channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.GetByIdKey); 
            /*channel.QueueBind(configs.Queue,
                configs.Exchange,
                configs.ResponseKey);*/
            
        }
    }
}
