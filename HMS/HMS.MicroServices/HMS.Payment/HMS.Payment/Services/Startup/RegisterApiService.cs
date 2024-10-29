using HMS.Payments.Core.Interfaces.Services;
using System.Threading;

namespace HMS.Payments.API.Services.Startup
{
    public class RegisterApiService : IHostedService
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        private readonly IConfiguration _configuration;
        private readonly Uri appAddress;
        private const string appName = "payments";

        public RegisterApiService(IServiceProvider provider, IConfiguration configuration)
        {
            _serviceDiscovery = provider.CreateScope().ServiceProvider.GetRequiredService<IServiceDiscovery>();
            _configuration = configuration;
            var address = _configuration["ASPNETCORE_URLS"] ?? throw new Exception("Não foi possivel capturar o endereço da api");
            appAddress = new Uri(address);
        }

        private async Task RegisterThisApi()
        {

            await _serviceDiscovery.RegisterService(new()
            {
                Address = appAddress.Host,
                Port = appAddress.Port,
                Name = appName,
                Check = new()
                {
                    HTTP = $"http://{appAddress.Host}:{appAddress.Port}/api/v1/Health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(10)
                }
            });
        }

        private async Task DeRegisterThisApi()
        {
            await _serviceDiscovery.DeRegisterService(appName);
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await RegisterThisApi();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await DeRegisterThisApi();
        }
    }
}
