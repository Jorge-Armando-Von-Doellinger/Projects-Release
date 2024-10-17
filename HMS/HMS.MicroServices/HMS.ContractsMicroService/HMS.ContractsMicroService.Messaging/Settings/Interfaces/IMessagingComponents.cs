namespace HMS.ContractsMicroService.Messaging.Settings.Interfaces
{
    public interface IMessagingComponents
    {
        string Queue { get; }
        string Exchange { get; }
        string TypeExchange { get; }
        string AddKey { get; }
        string DeleteKey { get; }
        string UpdateKey { get; }
        string ResponseKey { get; }
        string CurrentKey { get; }
    }
}
