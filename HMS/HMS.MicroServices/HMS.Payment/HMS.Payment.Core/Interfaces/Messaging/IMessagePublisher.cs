namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        void PublishResponseSync(object message);
        Task PublishSync<T>(T message, string exchange, string queue, string routingkey);
    }
}
