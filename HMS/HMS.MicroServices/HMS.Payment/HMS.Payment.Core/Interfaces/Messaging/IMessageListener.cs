namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessageListener
    {
        Task<Action<byte[]>> ListeningAsync();
        Action<byte[]> ListeningSync();
    }
}
