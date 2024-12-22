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
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config.JwtSecretkey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId), // userId
                new Claim(JwtRegisteredClaimNames.Email, userEmail) // email
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token, string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config.JwtSecretkey);
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, 
                ValidateAudience = false, 
                ClockSkew = TimeSpan.Zero // Para reduzir o tempo de tolerância durante a expiração do token
            };
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            if (validatedToken is JwtSecurityToken jwtToken)
            {
                var tokenUserId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return tokenUserId == userId;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

}