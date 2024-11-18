namespace HMS.Payments.Application.Settings;

public sealed class NotificationSettings
{
    public string Queue { get; set; }
    public string Exchange { get; set; }
    public string TypeExchange { get; set; }
}