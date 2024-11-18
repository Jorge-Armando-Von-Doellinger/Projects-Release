namespace HMS.Payments.Core.Interfaces.Messaging
{
    public interface IMessageListener
    {
        public ulong[] Tags {get; }
        public List<byte[]> MessageBytes { get; }
        Task ListeningAsync(CancellationToken cancellationToken);
    }
}
