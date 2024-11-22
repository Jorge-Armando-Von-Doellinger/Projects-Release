namespace HMS.Notification.Messaging.Settings;

public sealed class MessagingSettings
{
    public string Address { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public Dictionary<string, string> Queues { get; set; }
    public string TypeExchange { get; set; }
    public string Exchange { get; set; }
}