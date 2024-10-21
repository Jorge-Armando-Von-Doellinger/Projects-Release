using HMS.ContractsMicroService.API.Settings.Interfaces;

namespace HMS.ContractsMicroService.API.Settings
{
    public sealed class AppSettings : IAppSettings
    {
        public string ApplicationName { get; set; }
    }
}
