namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        void PublishResponseSync(object message);
        void PublishSync(object message, string exchange, string queue, string routingkey);
    }
}
