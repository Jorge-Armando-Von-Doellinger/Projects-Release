namespace HMS.ContractsMicroService.Infrastructure.Settings.Interfaces
{
    public interface IServiceDiscoverySettings
    {
        string KvKeySchemas { get; }
        string KvKeySettings { get; }
        string Uri { get; }
        string GetSchema(string nameof);
        string GetSettings(string nameof);
    }
}
