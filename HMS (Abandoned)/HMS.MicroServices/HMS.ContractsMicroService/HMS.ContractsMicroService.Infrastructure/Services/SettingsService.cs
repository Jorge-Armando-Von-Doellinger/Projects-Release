using HMS.ContractsMicroService.Core.Interfaces.Services;
using NJsonSchema;
using System.Text.Json;
using System.Text;
using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;
using HMS.ContractsMicroService.Core.Interfaces.Settings;

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

        public async Task<string> GetJsonSettings(string kvkey)
        {
            return await _serviceDiscovery.Get(_settings.GetSettings(kvkey));
        }

        public async Task<string> GetSchema(string kvkey)
        {
            var data = await _serviceDiscovery.Get(_settings.GetSchema(kvkey));
            return JsonSerializer.Deserialize<string>(data);
        }

        public async Task RegisterSchemas(Type type, string kvkey)
        {
            var schema = JsonSchema.FromType(type);
            await _serviceDiscovery.Register(schema.ToJson(), _settings.GetSchema(kvkey)); //aqui pede um object
        }

        public async Task RegisterSchemas(string json, string kvkey)
        {
            var schema = JsonSchema.FromSampleJson(json);
            await _serviceDiscovery.Register(schema.ToJson(), _settings.GetSchema(kvkey));
        }

        public async Task RegisterSettings(string json, string kvkey)
        {
            try
            {
                await _serviceDiscovery.Register(json, _settings.GetSettings(kvkey)); //aqui pede um object
            }
            catch (Exception ex)
            {
                Console.WriteLine("Não foi possivel converter e registrar seu json de configuração!");
            }
        }

        public Task UpdateSchemas(Type type, string key)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSettings(OnUpdatedSettings actions, string json, string key)
        {
            await RegisterSettings(json, key);
            actions.OnSettingsChanged(json);
        }
    }
}
