namespace HMS.ContractsMicroService.Messaging.Settings.Interfaces
{
    public interface IMessagingSettings
    {
        string HostName { get; }
        int Port { get; }
    }
}
