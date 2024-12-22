namespace HMS.Payments.Infrastructure;

public record ConnectionTest
{
    internal static readonly string connectionString = "Server=localhost,1433; " +
                                                       "Database=ProjectsRelease;" +
                                                       "User Id=SA;" +
                                                       "Password=YourStrong!Passw0rd;" +
                                                       "TrustServerCertificate=true";
}