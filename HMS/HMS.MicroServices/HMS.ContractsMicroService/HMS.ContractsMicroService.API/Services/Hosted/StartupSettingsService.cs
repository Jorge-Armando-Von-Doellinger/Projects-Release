using HMS.ContractsMicroService.API.Services.Data;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Nuget.Settings;
using System.Text.Json;

namespace HMS.ContractsMicroService.API.Services.Hosted
{
    public sealed class StartupSettingsService : IHostedService
    {
        private readonly IDiscoveryService _discoveryService;
        private readonly IHostEnvironment _configuration;

        public StartupSettingsService(IServiceProvider serviceProvider, IHostEnvironment configuration)
        {
            var service = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IDiscoveryService>();
            _discoveryService = service;
            _configuration = configuration;
        }

        private async Task GetSettings()
        {
            try
            {
                var settings = await _discoveryService.Get<AppSettings>(_configuration.ApplicationName);
                AppSettings.SetCurrentSettings(settings);
                
                SettingsStartupState.SetSettingsCompleted();
            }
            catch
            {
                Console.WriteLine("------------------");
                Console.WriteLine("Configurações não registradas!");
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await GetSettings();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
