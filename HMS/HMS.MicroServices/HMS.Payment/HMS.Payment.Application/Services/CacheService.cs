using HMS.Payments.Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace HMS.Payments.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public object? Get(string key)
        {
            return _cache.Get(key);
        }

        public T Get<T>(string key)
        {
            try
            {
                try
                {
                    return (T) _cache.Get(key);
                }
                catch
                {
                    return JsonSerializer.Deserialize<T>((string)_cache.Get(key));
                }
            }
            catch
            {
                throw new Exception("Não foi possvel pegar este cache");
            }
        }

        public void Set(string key, object value)
        {
            _cache.Set(key, value);
        }
    }
}
