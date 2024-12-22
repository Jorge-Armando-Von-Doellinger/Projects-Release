namespace OmniSphere.Users.Core.Interfaces.Hasher;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string password);
}