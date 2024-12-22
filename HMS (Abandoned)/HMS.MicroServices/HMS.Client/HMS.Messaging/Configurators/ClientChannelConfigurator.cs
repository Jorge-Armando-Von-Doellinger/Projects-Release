using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;
using System.Net;
using System.Threading.Channels;

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
        }

        public void SetConfigResponse(IModel channel, string keyResponse)
        {
            channel.QueueDeclare(queue: ResponseSettings.Queue,
            false,
            false,
            false,
            null);
            channel.ExchangeDeclare(exchange: ResponseSettings.Exchange,
                                     type: ResponseSettings.ExchangeType,
            false,
                                     false,
                                     null);

            channel.QueueBind(queue: ResponseSettings.Queue,
                               exchange: ResponseSettings.Exchange,
                               routingKey: keyResponse);
        }
    }
}
