namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessageListener
    {
        Task ListeningAsync(Func<byte[], Task> action);
        void ListeningSync(Action<byte[]> action);
    }
}
