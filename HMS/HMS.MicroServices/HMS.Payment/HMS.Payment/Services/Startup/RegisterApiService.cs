using HMS.Payments.Core.Interfaces.Services;
using System.Threading;

namespace HMS.Payments.API.Services.Startup
{
    public class RegisterApiService : IHostedService
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        private readonly IConfiguration _configuration;

        public RegisterApiService(IServiceDiscovery serviceDiscovery, IConfiguration configuration)
        {
            _serviceDiscovery = serviceDiscovery;
            _configuration = configuration;
        }

        private async Task RegisterThisApi()
        {
            var address = _configuration["ASPNETCORE_URLS"] ?? throw new Exception("Não foi possivel capturar o endereço da api");
            var uri = new Uri(address);
            await _serviceDiscovery.RegisterService(new()
            {
                Address = uri.Host,
                Port = uri.Port,
                Name = "payments",
                Check = new()
                {
                    HTTP = $"http://{uri.Host}:{uri.Port}/api/v1/Health",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(10)
                }
            });
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await RegisterThisApi();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Delay(1,  cancellationToken);
        }
    }
}
