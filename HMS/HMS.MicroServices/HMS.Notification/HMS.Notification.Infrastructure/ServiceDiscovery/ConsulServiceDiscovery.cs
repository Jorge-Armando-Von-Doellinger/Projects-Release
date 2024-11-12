using Consul;
using HMS.Notification.Core.Interfaces.ServiceDiscovery;
using HMS.Notification.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace HMS.Notification.Infrastructure.ServiceDiscovery;

public sealed class ConsulServiceDiscovery : IServiceDiscovery
{
    private readonly IOptionsMonitor<ServiceDiscoverySettings> _settings;
    private readonly IConsulClient _consulClient;

    public ConsulServiceDiscovery(IOptionsMonitor<ServiceDiscoverySettings> settings)
    {
        _settings = settings;
        _consulClient = new ConsulClient(configuration =>
        {
            configuration.Address = new Uri(_settings.CurrentValue.Address);
        });
    }
    public async Task RegisterServiceAsync(string serviceId, string serviceName, string address, string addressCheck)
    {
        var register = new AgentServiceRegistration()
        {
            Name = serviceName,
            Address = address,
            ID = serviceId,
            Check = new AgentServiceCheck()
            {
                HTTP = addressCheck,
                Interval = TimeSpan.FromSeconds(30),
                Timeout = TimeSpan.FromSeconds(60),
            }
        };
        await _consulClient.Agent.ServiceRegister(register);
    }

    public async Task UnregisterServiceAsync(string serviceId)
    {
        await _consulClient.Agent.ServiceDeregister(serviceId);
    }

    public async Task FindServiceAsync(string serviceId)
    {
        await _consulClient.Agent.GetServiceConfiguration(serviceId);
    }
}