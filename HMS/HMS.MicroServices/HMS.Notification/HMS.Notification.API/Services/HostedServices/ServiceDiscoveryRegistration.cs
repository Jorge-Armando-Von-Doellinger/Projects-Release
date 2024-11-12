using HMS.Notification.API.Settings;
using HMS.Notification.Core.Interfaces.ServiceDiscovery;
using Microsoft.Extensions.Options;

namespace HMS.Notification.API.Services.HostedServices;

public sealed class ServiceDiscoveryRegistration : IHostedService
{
    private readonly IServiceDiscovery _serviceDiscovery;
    private readonly IOptionsMonitor<ApiSettings> _settings;
    private readonly IConfiguration _configuration;

    public ServiceDiscoveryRegistration(IServiceDiscovery serviceDiscovery, IOptionsMonitor<ApiSettings> settings, IConfiguration configuration)
    {
        _serviceDiscovery = serviceDiscovery;
        _settings = settings;
        _configuration = configuration;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var currentSettings = _settings.CurrentValue;
        var address = _configuration["ASPNETCORE_URLS"] ?? throw new NullReferenceException();
        Console.WriteLine(address + currentSettings.ServiceHealthPath);
        await _serviceDiscovery.RegisterServiceAsync(currentSettings.ServiceId, 
            currentSettings.ServiceName, 
            address,
            address+currentSettings.ServiceHealthPath);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceDiscovery.UnregisterServiceAsync(_settings.CurrentValue.ServiceId);
    }
}