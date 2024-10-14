namespace Nuget.Settings.Messaging
{
    public interface IMessagingSystem
    {
        string Queue { get; }
        string Exchange { get; }
        string TypeExchange { get; }
        string AddKey { get; }
        string DeleteKey { get; }
        string UpdateKey { get; }
        string ResponseKey { get; }
        string CurrentKey { get; }

        string[] GetKeys();
    }
}
