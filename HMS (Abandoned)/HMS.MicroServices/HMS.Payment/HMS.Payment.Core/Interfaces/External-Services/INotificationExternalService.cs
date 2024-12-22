namespace HMS.Payments.Core.Interfaces.External_Services;

public interface INotificationExternalService
{
    Task SendEmail(string content, string title, string email);
}