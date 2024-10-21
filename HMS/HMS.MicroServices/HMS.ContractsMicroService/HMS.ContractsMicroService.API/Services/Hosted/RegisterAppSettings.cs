
using HMS.ContractsMicroService.API.Settings.Interfaces;
using HMS.ContractsMicroService.Core.Interfaces.Services;

namespace HMS.ContractsMicroService.API.Services.Hosted
{
    public sealed class RegisterAppSettings : IHostedService
    {
        private readonly ISettingsService _service;
        private readonly AppSettingsService _settingsService;
        private readonly IAppSettings _appSettings;
        private readonly IConfiguration _configuraiton;

        public RegisterAppSettings(ISettingsService service, IConfiguration configuraiton, AppSettingsService settingsService, IAppSettings appSettings = null)
        {
            _service = service;
            _configuraiton = configuraiton;
            _settingsService = settingsService;
            _appSettings = appSettings;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var settings = _settingsService.GetSettings(_configuraiton);
            await _service.RegisterSettings(settings.GetRawText(), _appSettings.ApplicationName);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
