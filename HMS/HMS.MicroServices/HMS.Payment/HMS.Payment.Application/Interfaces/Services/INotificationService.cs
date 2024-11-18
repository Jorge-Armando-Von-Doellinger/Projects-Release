namespace HMS.Payments.Application.Interfaces.Services;

public interface INotificationService
{
    Task SendNotificationAsync(string title, string message, string email);
}