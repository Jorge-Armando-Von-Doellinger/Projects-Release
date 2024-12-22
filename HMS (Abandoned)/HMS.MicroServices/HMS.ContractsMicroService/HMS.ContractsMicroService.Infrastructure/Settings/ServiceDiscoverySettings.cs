using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Infrastructure.Settings.Interfaces;
using Namotion.Reflection;
using System.Text.Json;

namespace HMS.ContractsMicroService.Infrastructure.Settings
{
    public sealed class ServiceDiscoverySettings : OnUpdatedSettings, IServiceDiscoverySettings
    {
        private string _kvKeySchemas;
        public string KvKeySchemas
        {
            get => _kvKeySchemas;
            set
            {
                AddUpdateEvent();
                _kvKeySchemas = value;
            }
        }
        private string _kvKeySettings;
        public string KvKeySettings
        {
            get => _kvKeySettings;
            set
            {
                AddUpdateEvent();
                _kvKeySettings = value;
            }
        }
        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                AddUpdateEvent();
                _address = value;
            }
        }


        private void AddUpdateEvent()
        {
            base.SettingsChanged += (json) =>
            {
                var updatedSettings = JsonSerializer.Deserialize<IServiceDiscoverySettings>(json);
                KvKeySettings = updatedSettings.KvKeySettings;
                KvKeySchemas = updatedSettings.KvKeySchemas;
                Address = updatedSettings.Address;
                Console.WriteLine(this.GetType().Name + " Is Updated");
            };
        }


        public string GetSchema(string nameof)
        {
            return KvKeySchemas + nameof;
        }

        public string GetSettings(string nameof)
        {
            return KvKeySettings + nameof;
        }
    }
}
