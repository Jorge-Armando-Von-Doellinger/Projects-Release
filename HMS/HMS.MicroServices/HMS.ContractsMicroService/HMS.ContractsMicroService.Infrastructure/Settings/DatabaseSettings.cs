using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;

namespace HMS.ContractsMicroService.Infrastructure.Settings
{
    public sealed class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}
