namespace OmniSphere.Users.Core.Interfaces.External_Services;

public interface IEmailExternalService
{
    Task SendEmailAsync(string title, string content, string email);
}