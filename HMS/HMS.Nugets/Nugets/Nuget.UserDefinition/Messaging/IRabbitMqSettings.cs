namespace Nuget.Settings.Messaging
{
    public interface IRabbitMqSettings
    {
        string HostName { get; }
        int Port { get; }
    }
}
