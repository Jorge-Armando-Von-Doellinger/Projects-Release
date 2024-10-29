namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishResponseAsync(object message);
        Task PublishAsync(object message, string exchange, string queue, string routingkey);
    }
}
