using HMS.ContractsMicroService.Core.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HMS.ContractsMicroService.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly List<string> _keys = new();
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public T? Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out var value))
                if (value is T TValue)
                    return TValue;
            return default;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveAll()
        {
            _keys.ForEach(key =>
            {
                _keys.Remove(key);
                Remove(key);
            });
        }

        public void Set<T>(string key, T value)
        {
            _keys.Add(key);
            _cache.Set(key, value);
        }
    }
}
