using Microsoft.Extensions.Caching.Memory;
using Nuget.Settings;
using Nuget.Settings.Messaging;

namespace HMS.ContractsMicroService.Messaging.Services
{
    public sealed class CacheSettingsService
    {
        private readonly IMemoryCache _cache;

        public CacheSettingsService(IMemoryCache cache)
        {
            _cache = cache;
        }

        internal RabbitMqSettings? GetMessagingSettings()
        {
            if (_cache.TryGetValue("settings", out var settings) == false)
                return null;
            return ((AppSettings)settings).RabbitMq;
        }
    }
}
