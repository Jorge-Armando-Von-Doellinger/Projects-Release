using HMS.Payments.Core.Data;
using HMS.Payments.Core.Entity;

namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message, string exchange, string queue, string routingkey);
        Task ToRetryQueue(Message message);
        Task ToDeadLetterQueueAsync<T>(T message);
    }
}
