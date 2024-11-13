namespace HMS.Notification.gRPC.Settings;

public sealed class AppSettings
{
    public string AppName { get; set; }
    public string AppId { get; set; }
    public string[] Tags { get; set; }
    public string ServiceHealthPath { get; set; }
}