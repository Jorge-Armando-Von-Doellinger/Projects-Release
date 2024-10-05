namespace Nuget.Settings.ServiceDiscovery
{
    public sealed class ConsulSettings
    {
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
        public int Port { get; set; }
    }
}
