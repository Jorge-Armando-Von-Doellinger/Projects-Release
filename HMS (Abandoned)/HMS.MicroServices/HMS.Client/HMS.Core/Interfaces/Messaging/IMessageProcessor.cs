namespace HMS.Core.Interfaces.Messaging
{
    public interface IMessageProcessor<T>
    {
        Task Process(T message);
    }
}
