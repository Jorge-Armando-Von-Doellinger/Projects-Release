using HMS.Payments.Core.Interfaces.Messaging;

namespace HMS.Payments.Messaging.Listeners
{
    public sealed class MessageListener : IMessageListener
    {
        public Task<Action<byte[]>> ListeningAsync()
        {
            return default;
        }

        public Action<byte[]> ListeningSync()
        {
            throw new NotImplementedException();
        }
    }
}
