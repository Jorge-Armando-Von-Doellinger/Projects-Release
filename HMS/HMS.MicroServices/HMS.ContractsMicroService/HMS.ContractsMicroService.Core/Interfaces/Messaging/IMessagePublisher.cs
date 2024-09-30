namespace HMS.ContractsMicroService.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        Task Publish(object data);
        Task PublishResponse(object data);
    }
}
