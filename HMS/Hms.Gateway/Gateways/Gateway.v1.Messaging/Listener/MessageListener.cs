using Gateway.v1.Core.Messaging.Listener;
using Gateway.v1.Messaging.Factory;
using Microsoft.Extensions.Hosting;
using Nuget.MessagingUtilities;
using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Gateway.v1.Messaging.Listener
{
    public sealed class MessageListener : IMessageListener
    {
        private readonly IModel _channel;
        public MessageListener(ChannelFactory factory)
        {
            _channel = factory.Channel;
        }
        public async Task<object> GetMessage(string baseResponse, Guid messageID)
        {
            var responseSettings = new ConfigureResponseRoutings();
            var consumer = new EventingBasicConsumer(_channel);
            string routingKey = responseSettings.GetResponseKey(baseResponse);
            Message message = default;
            consumer.Received += (sender, args) =>
            {
                if(args.RoutingKey.ToLower() == routingKey.ToLower())
                {
                    var body = args.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);
                    var messageDesserialized = JsonSerializer.Deserialize<Message>(json);
                    if(message.ID == messageID)
                    {
                        message = messageDesserialized;
                        _channel.BasicAck(args.DeliveryTag, false);
                    }
                }
            };
            _channel.QueueDeclare(ResponseSettings.Queue);
            _channel.QueueBind(ResponseSettings.Queue, 
                ResponseSettings.Exchange, 
                routingKey);

            _channel.BasicConsume(ResponseSettings.Queue,
                false,
                string.Empty,
                false,
                false,
                null,
                consumer);
            return message;
        }
    }
}
