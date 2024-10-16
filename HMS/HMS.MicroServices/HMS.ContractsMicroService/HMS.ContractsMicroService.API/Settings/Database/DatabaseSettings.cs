using Nuget.Settings.Database;

namespace HMS.ContractsMicroService.API.Settings.Database
{
    public class DatabaseSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
    }
}
