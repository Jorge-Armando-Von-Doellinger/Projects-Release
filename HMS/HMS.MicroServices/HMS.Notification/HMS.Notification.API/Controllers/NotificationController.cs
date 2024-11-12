using System.ComponentModel.DataAnnotations;
using HMS.Notification.Application.DTOs;
using HMS.Notification.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Notification.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public sealed class NotificationController : Controller
{
    private readonly INotificationManager _notificationManager;

    public NotificationController(INotificationManager notificationManager)
    {
        _notificationManager = notificationManager;
    }

    [HttpPost]
    public async Task<IActionResult> Post(NotificationDTO notificationDto, string email)
    {
        await _notificationManager.SendAsync(notificationDto, email);
        return Ok("Success");
    }
}