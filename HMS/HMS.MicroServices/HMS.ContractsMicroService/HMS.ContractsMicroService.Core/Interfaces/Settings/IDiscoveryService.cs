namespace HMS.ContractsMicroService.Core.Interfaces.Settings
{
    public interface IDiscoveryService
    {
        Task<T> Get<T>(string myAppName);
        Task Register(object settings, string myAppName);
        Task Put(object settings, string myAppName);
        Task Delete(string myAppName);
    }
}
