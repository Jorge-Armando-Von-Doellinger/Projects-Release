using Consul;
using HMS.ContractsMicroService.API.Services.Data;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Nuget.Settings;
using Nuget.Settings.Messaging;
using System.Text.Json;

namespace HMS.ContractsMicroService.API.Services.Hosted
{
    public sealed class StartupSettingsService : IHostedService
    {
        private readonly IDiscoveryService _discoveryService;
        private readonly IHostEnvironment _enviromentConfiguration;
        private readonly IConfiguration _configuration;

        public StartupSettingsService(IServiceProvider serviceProvider, IHostEnvironment enviromentConfiguration, IConfiguration configuration)
        {
            var service = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IDiscoveryService>();
            _discoveryService = service;
            _enviromentConfiguration = enviromentConfiguration;
            _configuration = configuration;
        }

        private async Task GetSettings()
        {
            try
            {
                var settings = await _discoveryService.Get<AppSettings>()
                    ?? _configuration.GetSection("DefaultAppSettings").Get<AppSettings>()
                    ?? throw new InvalidDataException("Nenhuma configuração pode ser encontrada e/ou utilizada.");
                AppSettings.SetCurrentSettings(settings); // Registra as configurações padrões do app
                
                await _discoveryService.Put(settings); // Registra as configurações no consul
                SettingsStartupState.SetSettingsCompleted(); // Permite que os outros servicos que necessitam dessas configurações
                                                             // possam inicar seu trabalho
            }
            catch(Exception ex)
            {
                Console.WriteLine("--------------------\n"+ex.Message+"\n------------------------");
                Console.WriteLine("------------------");
                Console.WriteLine("Erro ao configurar o app!");
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
