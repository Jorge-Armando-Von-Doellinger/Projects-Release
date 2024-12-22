namespace OmniSphere.Authentication.Core.Interfaces.Services;

public interface ITokenJwtService
{
    string GenerateToken(string userId, string userEmail);
    bool ValidateToken(string token, string userId);
}