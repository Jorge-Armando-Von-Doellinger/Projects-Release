using OmniSphere.Authentication.Core.Interfaces.TokenCacheService;
using StackExchange.Redis;

namespace OmniSphere.Authentication.Infrastructure.Implementations.TokenCacheService;

public class RedisTokenCacheService : ITokenCacheService
{
    private readonly ConnectionMultiplexer _connection;
    private readonly IDatabase _database;
    public RedisTokenCacheService()
    {
         _connection = ConnectionMultiplexer.Connect("localhost");
         _database = _connection.GetDatabase();
    }

    public async Task<string> GetTokenAsync(string userId)
    {
        var value = await _database.StringGetAsync(userId);
        return value.HasValue ? value.ToString() : string.Empty;
    }

    public async Task StoreTokenAsync(string token, string userId, TimeSpan expiration)
    {
        await _database.KeyDeleteAsync("*");
        await _database.StringSetAsync(userId, token, expiration);
    }
}