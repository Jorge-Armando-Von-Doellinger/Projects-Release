namespace OmniSphere.Authentication.Core.Interfaces.TokenCacheService;

public interface ITokenCacheService
{
    Task<string> GetTokenAsync(string userId); 
    Task StoreTokenAsync(string token, string userId, TimeSpan expiration);
}