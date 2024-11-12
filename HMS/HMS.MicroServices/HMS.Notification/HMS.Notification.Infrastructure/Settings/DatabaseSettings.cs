namespace HMS.Notification.Infrastructure.Settings;

public sealed class DatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string NotificationCollection { get; set; }
}