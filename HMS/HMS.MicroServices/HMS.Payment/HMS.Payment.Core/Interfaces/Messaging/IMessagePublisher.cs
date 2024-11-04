namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        void PublishResponseSync(object message);
        void PublishSync<T>(T message, string exchange, string queue, string routingkey);
    }
}
