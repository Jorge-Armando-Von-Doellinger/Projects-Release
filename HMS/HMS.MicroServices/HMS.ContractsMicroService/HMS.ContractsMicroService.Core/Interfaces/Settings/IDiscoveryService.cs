namespace HMS.ContractsMicroService.Core.Interfaces.Settings
{
    public interface IDiscoveryService
    {
        Task<T> Get<T>();
        Task Register(object settings);
        Task Put(object settings);
        Task Delete();
    }
}
