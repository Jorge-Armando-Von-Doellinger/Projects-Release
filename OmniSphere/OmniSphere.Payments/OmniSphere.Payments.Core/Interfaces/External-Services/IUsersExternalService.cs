namespace OmniSphere.Payments.Core.Interfaces.External_Services;

public interface IUsersExternalService
{
    Task<bool> ExistsUserAsync(string accountId);
}