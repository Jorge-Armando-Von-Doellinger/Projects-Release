using HMS.ContractsMicroService.Core.Interfaces.Services;
using NJsonSchema;
using System.Text.Json;
using System.Text;
using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;

namespace HMS.ContractsMicroService.Infrastructure.Services
{
    public sealed class SettingsService : ISettingsService
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        private readonly IServiceDiscoverySettings _settings;

        public SettingsService(IServiceDiscovery serviceDiscovery, IServiceDiscoverySettings settings)
        {
            _serviceDiscovery = serviceDiscovery;
            _settings = settings;
        }
        public async Task RegisterSchemas(Type type, string key)
        {

            var schema = JsonSchema.FromType(type);
            await _serviceDiscovery.Register(schema.ToJson(), _settings.GetSchema(key)); //aqui pede um object

        }

        public async Task RegisterSettings(string json, string key)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            using (var doc = await JsonDocument.ParseAsync(stream))
            {
                await _serviceDiscovery.Register(json, _settings.GetSettings(key)); //aqui pede um object
            }
        }
    }
}
