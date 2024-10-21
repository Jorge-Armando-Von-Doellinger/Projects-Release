namespace HMS.ContractsMicroService.Core.Interfaces.Services
{
    public interface IServiceDiscovery
    {
        Task<T> Get<T>(string kvkey);
        Task Register(object settings, string kvkey);
        Task Put(object settings, string kvkey);
        Task Delete(string kvkey);
    }
}
