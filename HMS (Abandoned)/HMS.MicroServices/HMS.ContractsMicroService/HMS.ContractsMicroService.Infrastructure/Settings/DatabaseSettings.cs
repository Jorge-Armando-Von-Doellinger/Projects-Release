using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;
using System.Text.Json;

namespace HMS.ContractsMicroService.Infrastructure.Settings
{
    public sealed class DatabaseSettings : OnUpdatedSettings, IDatabaseSettings
    {
        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                AddUpdateEvent();
            }
        }
        private void AddUpdateEvent()
        {
            SettingsChanged += (json) =>
            {
                var updatedSettings = JsonSerializer.Deserialize<IDatabaseSettings>(json);
                ConnectionString = updatedSettings.ConnectionString;
                Console.WriteLine(this.GetType().Name + " Is Updated ");
            };
        }

    }
}

