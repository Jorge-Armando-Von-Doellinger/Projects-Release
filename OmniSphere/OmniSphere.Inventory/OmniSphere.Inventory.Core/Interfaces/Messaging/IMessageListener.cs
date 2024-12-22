namespace OmniSphere.Inventory.Core.Interfaces.Messaging;

public interface IMessageListener
{
    Task StartListeningAsync(CancellationToken cancellationToken); 
}