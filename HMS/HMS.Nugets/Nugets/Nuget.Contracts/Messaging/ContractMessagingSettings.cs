namespace Nuget.Contracts.Messaging
{
    public sealed class ContractMessagingSettings
    {
        public ContractMessagingSettings()
        {
            var baseName = "contracts";
            Queue = baseName;
            Exchange = baseName;
            TypeExchange = "topics";

            AddKey = BaseKey + "add";
            DeleteKey = BaseKey + "delete";
            UpdateKey = BaseKey + "update";
        }
        private string BaseKey { get; } = "contract.";
        public string Queue { get; set; }
        public string Exchange { get; set; }
        public string TypeExchange { get; set; }
        public string AddKey { get; set; }
        public string DeleteKey { get; set; }
        public string UpdateKey { get; set; }
        public string ResponseKey { get; set; }
        public string CurrentKey { get; private set; }
        public void SetCurrentKey(string currentKey) => CurrentKey = currentKey;
    }
}
