namespace OmniSphere.Products.Core.Interfaces.External_Services;

public interface INotificationService
{
    Task SendEmailAsync(string title, string message, string email);
}   