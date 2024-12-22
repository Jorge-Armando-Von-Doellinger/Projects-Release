namespace OmniSphere.Authentication.Application.Interfaces.UseCases;

public interface IAuthUseCase
{
    Task<string?> ValidateCredentialsAsync(string email, string password);
    Task<bool> ValidateTokenAsync(string userId, string token);
}