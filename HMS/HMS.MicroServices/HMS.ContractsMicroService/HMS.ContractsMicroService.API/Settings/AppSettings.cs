using HMS.ContractsMicroService.API.Settings.Interfaces;
using HMS.ContractsMicroService.Core.Interfaces.Settings;
using System.Text.Json;

namespace HMS.ContractsMicroService.API.Settings
{
    public sealed class AppSettings : OnUpdatedSettings, IAppSettings
    {
        private string _appName;
        public string ApplicationName
        {
            get => _appName;
            set
            {
                _appName = value;
            }
        }

        private void AddUpdateEvent()
        {
            base.SettingsChanged += (json) =>
            {
                var data = JsonSerializer.Deserialize<AppSettings>(json);
                ApplicationName = data.ApplicationName;
            };
        }
    }
}
