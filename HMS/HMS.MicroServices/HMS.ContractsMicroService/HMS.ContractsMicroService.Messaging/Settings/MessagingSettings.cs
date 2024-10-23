using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using System.Text.Json;

namespace HMS.ContractsMicroService.Messaging.Settings
{
    public sealed class MessagingSettings : OnUpdatedSettings, IMessagingSettings
    {
        private string _hostname;
        public string HostName
        {
            get => _hostname;
            set
            {
                AddUpdateEvent();
                _hostname = value;
            }
        }
        private int _port;
        public int Port
        {
            get => _port;
            set
            {
                AddUpdateEvent();
                _port = value;
            }
        }
        private void AddUpdateEvent()
        {
            base.SettingsChanged += (json) =>
            {
                var updatedSettings = JsonSerializer.Deserialize<IMessagingSettings>(json);
                HostName = updatedSettings.HostName;
                Port = updatedSettings.Port;
                Console.WriteLine(this.GetType().Name + " Is Updated");
            };
        }
    }
}
