using HMS.ContractsMicroService.API.Services.Data;
using HMS.ContractsMicroService.Application.Settings;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using Microsoft.Extensions.Caching.Memory;
using Nuget.Settings;

namespace HMS.ContractsMicroService.API.Services.Hosted
{
    public sealed class StartupSettingsService : IHostedService
    {
        private readonly IDiscoveryService _discoveryService;
        private readonly IHostEnvironment _enviromentConfiguration;
        private readonly IConfiguration _configuration;
        private readonly IAppSettings _settings;

        public StartupSettingsService(
            IServiceProvider serviceProvider, 
            IHostEnvironment enviromentConfiguration, 
            IConfiguration configuration,
            IAppSettings settings)
        {
            var service = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IDiscoveryService>();
            _discoveryService = service;
            _enviromentConfiguration = enviromentConfiguration;
            _configuration = configuration;
            _settings = settings;
        }

        private async Task GetSettings()
        {
            try
            {
                var settings = await _discoveryService.Get<AppSettings>()
                    ?? _configuration.GetSection("DefaultAppSettings").Get<AppSettings>()
                    ?? throw new InvalidDataException("Nenhuma configuração pode ser encontrada e/ou utilizada.");
                Console.WriteLine(settings.ApplicationName);
                //----------------------------------
                _settings.SetCurrentSettings(settings); // Registra as configurações padrões do app
                await _discoveryService.Put(settings); // Registra as configurações no consul
                SettingsStartupState.SetSettingsCompleted(); // Permite que os outros servicos que necessitam dessas configurações
                                                             // possam inicar seu trabalho
            }
            catch(Exception ex)
            {
                Console.WriteLine("--------------------\n"+ex.Message+"\n------------------------");
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
