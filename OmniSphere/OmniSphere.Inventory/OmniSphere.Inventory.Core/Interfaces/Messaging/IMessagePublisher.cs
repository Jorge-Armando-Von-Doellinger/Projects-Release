namespace OmniSphere.Inventory.Core.Interfaces.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, string exchange, string queue, string routingkey) where T : class;
}