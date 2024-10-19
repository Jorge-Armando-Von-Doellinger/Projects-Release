namespace HMS.ContractsMicroService.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        Task Publish(object data, string key);
        Task PublishResponse(object data, string responseKey);
    }
}
