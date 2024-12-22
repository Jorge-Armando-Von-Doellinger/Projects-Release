using HMS.ContractsMicroService.Core.Interfaces.Settings;
using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using System.Text.Json;

namespace HMS.ContractsMicroService.Messaging.Settings
{
    public sealed class MessagingComponents : OnUpdatedSettings,IMessagingComponents
    {
        public MessagingComponents()
        {
        }
        private string _queue;

        public string Queue
        {
            get => _queue;
            set
            {
                AddUpdateEvent();
                _queue = value;
            }
        }
        private string _exchange;
        public string Exchange
        {
            get => _exchange;
            set
            {
                AddUpdateEvent();
                _exchange = value;
            }
        }

        public string _typeExchange;
        public string TypeExchange
        {
            get => _typeExchange;
            set
            {
                AddUpdateEvent();
                _typeExchange = value;
            }
        }
        private string _addKey, _updateKey, _deleteKey, _responseKey;
        public string AddKey
        {
            get => _addKey;
            set
            {
                AddUpdateEvent();
                _addKey = value;
            }
        }

        public string DeleteKey
        {
            get => _deleteKey;
            set
            {
                AddUpdateEvent();
                _deleteKey = value;
            }
        }

        public string UpdateKey
        {
            get => _updateKey;
            set
            {
                AddUpdateEvent();
                _updateKey = value;
            }
        }

        public string ResponseKey
        {
            get => _responseKey;
            set
            {
                AddUpdateEvent();
                _responseKey = value;
            }
        }

        public string CurrentKey { get; set; }

        public string[] Keys { get; private set; } = new string[4];

            
        public void SetKeys()
        {
            Keys = new string[4] { AddKey, UpdateKey, DeleteKey, UpdateKey };
        }
        private void AddUpdateEvent()
        {
            base.SettingsChanged += (json) =>
            {
                var updatedSettings = JsonSerializer.Deserialize<IMessagingComponents>(json);
                Console.WriteLine(this.GetType().Name);
            };
        }
    }
}
