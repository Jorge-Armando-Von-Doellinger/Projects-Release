using OmniSphere.Authentication.Core.Interfaces.External_Services;

namespace OmniSphere.Authentication.Infrastructure.Implementations.External_Services;

public class ValidateCredentialsExternalService : ILoginExternalService
{
    public ValidateCredentialsExternalService()
    {
        
    }
    public async Task<bool> ValidateCredentialsAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetUserIdByCredentialsAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}   