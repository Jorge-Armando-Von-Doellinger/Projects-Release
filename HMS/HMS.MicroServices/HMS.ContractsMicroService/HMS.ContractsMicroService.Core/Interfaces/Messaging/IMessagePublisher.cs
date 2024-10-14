namespace HMS.ContractsMicroService.Core.Interfaces.Messaging
{
    public interface IMessagePublisher<Settings> where Settings : class
    {
        Task Publish(object data, Settings settings);
        Task PublishResponse(object data, Settings settings);
    }
}
