namespace HMS.Notification.Core.Interfaces.ServiceDiscovery;

public interface IServiceDiscovery
{
    Task RegisterServiceAsync(string serviceId, string serviceName, string address, string addressCheck);
    Task UnregisterServiceAsync(string serviceId);
    Task FindServiceAsync(string serviceId);
}