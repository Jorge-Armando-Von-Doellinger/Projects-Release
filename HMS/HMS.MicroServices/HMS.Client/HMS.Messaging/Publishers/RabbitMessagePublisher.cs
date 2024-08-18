using HMS.Core.Interfaces.Messaging;
using HMS.Messaging.Factorys;
using HMS.Messaging.Tests;
using Nuget.Client.MessagingSettings;
using Nuget.MessagingUtilities.MessageSettings;
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
                var configureResponse = new ConfigureResponseRoutings();
                var clientConfig = new ClientMessagingSettings();
                string keyResponse = configureResponse.GetResponseKey(clientConfig.ResponseBase);

                string dataSerialized = JsonSerializer.Serialize(data);
                byte[] dataBytes = Encoding.UTF8.GetBytes(dataSerialized);

                _channel.QueueDeclare(queue: ResponseSettings.Queue);
                _channel.ExchangeDeclare(exchange: ResponseSettings.Exchange, 
                                         type: ResponseSettings.ExchangeType);

                _channel.QueueBind(queue: ResponseSettings.Queue, 
                                   exchange: ResponseSettings.Exchange,
                                   routingKey: keyResponse);
                _channel.BasicPublish(exchange: ResponseSettings.Exchange,
                    routingKey: keyResponse,
                    mandatory: false,
                    basicProperties: null,
                    body: dataBytes);
                //await new ResponseQueueTest().ResponseQueue(_channel);
                return await Task.FromResult(true);
            }
            catch
            {
                throw;
                return false;
            }
        }
    }
}
