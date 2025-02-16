using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OmniSphere.Authentication.Application.Configs;
using OmniSphere.Authentication.Core.Interfaces.Services;

namespace OmniSphere.Authentication.Application.Implementations.Services;

public class TokenJwtService : ITokenJwtService
{
    private readonly SecureKeyConfig _config;

    public TokenJwtService(IOptionsMonitor<SecureKeyConfig> config)
    {
        _config = config.CurrentValue;
    }

    public string GenerateToken(string userId, string userEmail)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, userEmail),
            new Claim("userId", userId),
            new Claim(JwtRegisteredClaimNames.Jti, userId),
        };
        var key = Encoding.UTF8.GetBytes(_config.JwtSecretkey);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetUserIdByJwtToken(string token)
    {
        string userId = null;
        var invalidTokenException = new SecurityTokenException("Invalid token");
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.JwtSecretkey);
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
            var principal = tokenHandler.ValidateToken(token, parameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtToken || 
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.Ordinal)) throw invalidTokenException;
            userId = principal.FindFirst("userId")?.Value ?? throw invalidTokenException;
            Console.WriteLine(principal.FindFirst("userId")?.Value);
            return userId;
        }
        catch
        {
            return null;
        }
    }
}