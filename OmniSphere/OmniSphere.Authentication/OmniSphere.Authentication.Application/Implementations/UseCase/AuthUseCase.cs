using OmniSphere.Authentication.Application.Interfaces.UseCases;
using OmniSphere.Authentication.Core.Interfaces.External_Services;
using OmniSphere.Authentication.Core.Interfaces.Services;
using OmniSphere.Authentication.Core.Interfaces.TokenCacheService;

namespace OmniSphere.Authentication.Application.Implementations.UseCase;

public class AuthUseCase : IAuthUseCase
{
    private readonly ILoginExternalService _loginService;
    private readonly ITokenJwtService _service;
    private readonly ITokenCacheService _tokenCacheService;

    public AuthUseCase(ILoginExternalService loginService,
        ITokenJwtService service,
        ITokenCacheService tokenCacheService)
    {
        _loginService = loginService;
        _service = service;
        _tokenCacheService = tokenCacheService;
    }

    public async Task<string?> GenerateTokenAsync(string email, string password)
    {
        var userId = await _loginService.GetUserIdByCredentialsAsync(email, password);
        if (string.IsNullOrEmpty(userId) || userId.Length == 0) return null;
        var token = _service.GenerateToken(userId, email);
        await _tokenCacheService.StoreTokenAsync(token, userId, TimeSpan.FromHours(1));
        return token;
    }

    public async Task<string> GetUserIdByTokenAsync(string token)
    {
        var userId = _service.GetUserIdByJwtToken(token);
        if(string.IsNullOrEmpty(userId) || userId.Length == 0) return null;
        var cachedToken = await _tokenCacheService.GetTokenAsync(userId);
        if(string.IsNullOrEmpty(cachedToken) || cachedToken.Length == 0) return null;
        return userId;
    }

}