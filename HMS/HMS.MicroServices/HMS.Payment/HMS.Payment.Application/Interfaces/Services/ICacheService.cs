namespace HMS.Payments.Application.Interfaces.Services
{
    public interface ICacheService
    {
        void Set(string key, object value);
        object? Get(string key);
        T? Get<T>(string key);
    }
}
