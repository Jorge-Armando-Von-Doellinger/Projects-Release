namespace HMS.ContractsMicroService.Core.Interfaces.Services
{
    public interface ISettingsService
    {
        Task RegisterSettings(string data, string key);
        Task RegisterSchemas(Type type, string key);
    }
}
