namespace Nuget.Settings.ServiceDiscovery
{
    public interface IConsulSettings
    {
        public string KvKey { get; }
        public string ServiceId { get; }
        public string ServiceName { get; }
        public string ServiceAddress { get; }
        public int Port { get; }
    }
}
