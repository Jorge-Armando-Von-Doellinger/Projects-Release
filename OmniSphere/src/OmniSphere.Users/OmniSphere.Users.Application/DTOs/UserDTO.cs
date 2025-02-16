using System.ComponentModel.DataAnnotations;

namespace OmniSphere.Users.Application.DTOs;

public class UserDTO
{
    [MinLength(3)]
    public string Username { get; set; }
    [MinLength(8)]
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}