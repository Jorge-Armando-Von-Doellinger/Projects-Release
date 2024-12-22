namespace HMS.Notification.Core.Interfaces.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync(object message, string exchange, string queue, string routingKey);
    Task PublishToRetryAsync(object message);
}