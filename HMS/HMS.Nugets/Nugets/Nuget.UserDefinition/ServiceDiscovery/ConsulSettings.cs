namespace Nuget.Settings.ServiceDiscovery
{
    public class ConsulSettings
    {
        public string KvKey { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceAddress { get; set; }
        public int Port { get; set; }
    }
}
