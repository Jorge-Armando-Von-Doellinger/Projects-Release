namespace HMS.ContractsMicroService.Messaging.Settings.Interfaces
{
    public interface IMessagingSystem
    {
        Dictionary<string, IMessagingComponents> Components { get; }
    }
}
