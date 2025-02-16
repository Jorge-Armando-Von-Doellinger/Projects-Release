using System.Security.Cryptography;
using OmniSphere.Users.Core.Interfaces.Hasher;

namespace OmniSphere.Users.Application.Implementations.Hasher;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        var salt = GenerateSalt();
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            var hash = pbkdf2.GetBytes(32);
            var hashBytes = new byte[64];
            // Copia o SALT para o hashBytes, a partir do index 0
            // Determina a onde começa a copia dos bytes e o tamanho da array de bytes
            Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length); // origem, onde começa a copia dos dados de origem, os dados a serem copiados, a partir de qual index ele será copiado e o tamnho da array que será copiado
            Buffer.BlockCopy(hash, 0, hashBytes, salt.Length, hash.Length);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        var hashBytes = Convert.FromBase64String(hashedPassword);
        var salt = new byte[16];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, salt.Length);
        var storedPasswordHash = new byte[32];
        Buffer.BlockCopy(hashBytes, salt.Length, storedPasswordHash, 0, storedPasswordHash.Length);
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            var computedHash = pbkdf2.GetBytes(32);

            // Comparando o hash gerado com o armazenado
            for (var i = 0; i < 32; i++)
            {
                if (computedHash[i] != storedPasswordHash[i])
                {
                    return false; 
                }
            }
        }

        return true;  
    }
    private byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }
        
    }
}