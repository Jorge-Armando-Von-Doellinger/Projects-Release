using HMS.Payments.Core.Data;

namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message, string exchange, string queue, string routingkey);
        Task ReRepublishAsync(MessageData message, string exchange, string queue, string routingkey);
    }
}
