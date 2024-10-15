using Nuget.Settings.ServiceDiscovery;

namespace HMS.ContractsMicroService.Application.Settings.ServiceDiscovery
{
    public class ServiceDiscoverySettings : IConsulSettings
    {
        public string KvKey { get; protected set; }

        public string ServiceId { get; protected set; }

        public string ServiceName { get; protected set; }

        public string ServiceAddress { get; protected set; }

        public int Port { get; protected set; }
    }
}
