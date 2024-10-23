namespace HMS.ContractsMicroService.Core.Interfaces.Settings
{
    public abstract class OnUpdatedSettings
    {
        protected event Action<string> SettingsChanged;
        public void OnSettingsChanged(string json)
        {
            SettingsChanged?.Invoke(json);
        }
    }
}
