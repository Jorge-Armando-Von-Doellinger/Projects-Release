namespace OmniSphere.Authentication.Core.Interfaces.External_Services;

public interface ILoginExternalService
{
    Task<string> GetUserIdByCredentialsAsync(string email, string password);
}