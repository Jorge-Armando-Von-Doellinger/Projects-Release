namespace HMS.ContractsMicroService.Infrastructure
{
    public record ConnectionTest
    {
        internal static readonly string connectionString = "Server=DESKTOP-6Q148IQ\\MSSQLSERVER01;" +
            "Database=ProjectsRelease;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;";
    }
}
