using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Context;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HMS.Payments.Messaging.Publisher
{
    public sealed class MessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;
        private readonly MessagingSystem _messagingSystem;

        public MessagePublisher(IModel channel, RabbitContext context, IOptionsMonitor<MessagingSystem> messagingSystem)
        {
            _channel = channel;
            _messagingSystem = messagingSystem.CurrentValue;
        }

        public void PublishSync<T>(T message, string exchange, string queue, string routingkey)
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null
            };
            var serialized = JsonSerializer.Serialize(message, jsonOptions);
            var bytes = Encoding.UTF8.GetBytes(serialized);
            _channel.BasicPublish(exchange, routingkey, null, bytes);
        }

        public void PublishResponseSync(object message)
        {
            throw new NotImplementedException();
        }
    }
}
