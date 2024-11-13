using HMS.Notification.Core.Interfaces.ServiceDiscovery;
using HMS.Notification.gRPC.Settings;
using Microsoft.Extensions.Options;

namespace HMS.Notification.gRPC.Services.HostedServices;

public sealed class ServiceDiscoveryRegistration : IHostedService
{
    private readonly IServiceDiscovery _serviceDiscovery;
    private readonly IOptionsMonitor<AppSettings> _settings;
    private readonly IConfiguration _configuration;

    public ServiceDiscoveryRegistration(IServiceDiscovery serviceDiscovery, IOptionsMonitor<AppSettings> settings, IConfiguration configuration)
    {
        _serviceDiscovery = serviceDiscovery;
        _settings = settings;
        _configuration = configuration;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_settings.CurrentValue != null)
        {
            var currentSettings = _settings.CurrentValue;
            var address = _configuration["ASPNETCORE_URLS"] ?? throw new NullReferenceException();
            await _serviceDiscovery.RegisterServiceAsync(currentSettings.AppId, 
                currentSettings.AppName, 
                address,
                address+currentSettings.ServiceHealthPath);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _serviceDiscovery.UnregisterServiceAsync(_settings.CurrentValue.AppId);
    }
}