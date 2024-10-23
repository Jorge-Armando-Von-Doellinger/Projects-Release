using HMS.ContractsMicroService.Core.Interfaces.Settings;

namespace HMS.ContractsMicroService.Core.Interfaces.Services
{
    public interface ISettingsService
    {
        Task RegisterSettings(string data, string kvkey);
        Task RegisterSchemas(Type type, string kvkey);
        Task<string> GetSchema(string kvkey);
        Task UpdateSettings(OnUpdatedSettings updateEvent, string data, string key);
        Task UpdateSchemas(Type type, string kvkey);

        Task<string> GetJsonSettings(string kvkey);
    }
}
