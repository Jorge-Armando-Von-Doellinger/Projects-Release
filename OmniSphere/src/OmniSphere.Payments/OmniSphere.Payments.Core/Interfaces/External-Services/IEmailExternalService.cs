namespace OmniSphere.Payments.Core.Interfaces.External_Services;

public interface IEmailExternalService
{
    Task SendEmailAsync(string title, string message, string email);
}