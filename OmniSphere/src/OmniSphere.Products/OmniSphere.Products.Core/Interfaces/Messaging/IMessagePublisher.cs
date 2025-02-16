namespace OmniSphere.Products.Core.Interfaces.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message) where T : class;
}