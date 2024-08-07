using HMS.Core.Interfaces.Messaging;
using HMS.Messaging.Factorys;
using Nuget.Clients.DTOs.Mensaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HMS.Messaging.Publishers
{
    public class RabbitMessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;
        public RabbitMessagePublisher(ClientChannelFactory factory) 
        { 
            _channel = factory.Channel;
        }
        public async Task<bool> PublishMessage(object data)
        {
            try
            {
                var configs = new ClientMessagingConfigs();
                string dataSerialized = JsonSerializer.Serialize(data);
                byte[] dataBytes = Encoding.UTF8.GetBytes(dataSerialized);
                _channel.BasicPublish(exchange: configs.Exchange,
                    routingKey: configs.ResponseKey,
                    mandatory: false,
                    basicProperties: null,
                    body: dataBytes);
                return await Task.FromResult(true);
            }
            catch
            {
                return false;
            }
        }
    }
}
