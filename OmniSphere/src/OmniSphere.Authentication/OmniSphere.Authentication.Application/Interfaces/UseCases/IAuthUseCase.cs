namespace OmniSphere.Authentication.Application.Interfaces.UseCases;

public interface IAuthUseCase
{
    Task<string?> GenerateTokenAsync(string email, string password);
    Task<string> GetUserIdByTokenAsync(string token);
}