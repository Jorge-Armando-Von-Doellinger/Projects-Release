using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using System.Text.Json;
using System.Windows.Markup;

namespace HMS.ContractsMicroService.Messaging.Settings
{
    public sealed class MessagingSystem : OnUpdatedSettings, IMessagingSystem
    {
        private Dictionary<string, IMessagingComponents> _messagingComponents;
        public Dictionary<string, IMessagingComponents> Components
        {
            get => _messagingComponents;
            set => _messagingComponents = value;
        }

        private void AddUpdateEvent()
        {
            base.SettingsChanged += (json) =>
            {
                var updatedSettings = JsonSerializer.Deserialize<Dictionary<string, MessagingComponents>>(json);
                var settings = updatedSettings.ToDictionary(x => x.Key, x => (IMessagingComponents)x.Value);
                _messagingComponents = settings;
                Console.WriteLine(this.GetType().Name + " Is Updated");
            };
        }
    }
}
