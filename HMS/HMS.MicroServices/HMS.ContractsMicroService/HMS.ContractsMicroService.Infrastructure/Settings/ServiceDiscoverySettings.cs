using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;

namespace HMS.ContractsMicroService.Infrastructure.Settings
{
    public sealed class ServiceDiscoverySettings : IServiceDiscoverySettings
    {
        public string KvKeySchemas { get; set; }

        public string KvKeySettings { get; set; }

        public string Uri { get; set; }

        public string GetSchema(string nameof)
        {
            return KvKeySchemas + nameof;
        }

        public string GetSettings(string nameof)
        {
            return KvKeySettings + nameof;
        }
    }
}
