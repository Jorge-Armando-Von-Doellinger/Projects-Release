namespace HMS.ContractsMicroService.Core.Interfaces.Services
{
    public interface ICacheService
    {
        T? Get<T>(string key);
        void Set<T>(string key, T value);
        void Remove(string key);
        void RemoveAll();
    }
}
