namespace OmniSphere.Authentication.Core.Interfaces.Services;

public interface ITokenJwtService
{
    string GenerateToken(string userId, string userEmail);
    string GetUserIdByJwtToken(string token);
}