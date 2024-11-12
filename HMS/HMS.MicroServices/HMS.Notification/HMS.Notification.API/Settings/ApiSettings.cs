namespace HMS.Notification.API.Settings;

public sealed class ApiSettings
{
    public string ServiceName { get; set; }
    public string ServiceHealthPath { get; set; }
    public string ServiceId { get; set; }
    public string[] Tags { get; set; }
}