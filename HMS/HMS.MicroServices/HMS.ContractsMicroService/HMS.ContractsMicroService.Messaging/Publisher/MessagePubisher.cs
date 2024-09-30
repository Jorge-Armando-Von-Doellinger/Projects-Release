using HMS.ContractsMicroService.Core.Interfaces.Messaging;

namespace HMS.ContractsMicroService.Messaging.Publisher
{
    public sealed class MessagePubisher : IMessagePublisher
    {
        public Task Publish(object data)
        {
            throw new NotImplementedException();
        }

        public Task PublishResponse(object data)
        {
            throw new NotImplementedException();
        }
    }
}
