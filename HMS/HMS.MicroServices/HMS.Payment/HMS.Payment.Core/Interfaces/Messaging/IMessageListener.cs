namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessageListener
    {
        Task ListeningAsync(CancellationToken cancellationToken);
    }
}
