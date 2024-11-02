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

        public void PublishSync(object message, string exchange, string queue, string routingkey)
        {
            var serialized = JsonSerializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(serialized);

            _channel.BasicPublish(exchange, routingkey, false, null, bytes);
        }

        public void PublishResponseSync(object message)
        {
            throw new NotImplementedException();
        }
    }
}
