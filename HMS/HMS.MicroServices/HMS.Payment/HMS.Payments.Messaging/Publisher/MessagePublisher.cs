using HMS.Payments.Core.Interfaces.Messaging;
using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Publisher
{
    public sealed class MessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;
        public MessagePublisher(IModel channel)
        {
            _channel = channel;
        }

        public Task PublishAsync(object message, string exchange, string queue, string routingkey)
        {
            throw new NotImplementedException();
        }

        public Task PublishResponseAsync(object message)
        {
            throw new NotImplementedException();
        }
    }
}
