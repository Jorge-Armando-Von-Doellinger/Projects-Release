using System.Net;
using System.Net.Mail;
using DotNetEnv;
using HMS.Notification.Core.Entity;
using HMS.Notification.Core.Interfaces.Services;

namespace HMS.Notification.Application.Services;

public sealed class NotificationService : INotificationService
{
    public async Task SendEmail(NotificationByEmail notification)
    {
        var email = Env.GetString("EMAIL_USER") ?? throw new ArgumentNullException("EMAIL_USER");
        var pass = Env.GetString("EMAIL_PASS") ?? throw new Exception();
        var port = Env.GetInt("SMTP_PORT");
        if(port <= 0) throw new Exception("SMTP Port is invalid");
        var host = Env.GetString("SMTP_SERVER") ?? throw new Exception("SMTP_SERVER");
        
        var client = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(email, pass),
            Timeout = 60 * 60,
            UseDefaultCredentials = false,
            EnableSsl = true
        };
        var message = new MailMessage()
        {
            From = new MailAddress(notification.Email, "HMS Notification"),
            Body = notification.Content,
            Subject = notification.Title,
            IsBodyHtml = true,
            Priority = MailPriority.Normal,
        };
        message.To.Add(notification.Email);
        await client.SendMailAsync(message);
    }
}