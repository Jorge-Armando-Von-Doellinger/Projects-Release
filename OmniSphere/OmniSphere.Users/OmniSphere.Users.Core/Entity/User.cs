using System.ComponentModel.DataAnnotations;

namespace OmniSphere.Users.Core.Entity;

public class User: BaseEntity
{
    [MinLength(3)]
    public string Username { get; set; }
    [MinLength(8)]
    public string Password { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}