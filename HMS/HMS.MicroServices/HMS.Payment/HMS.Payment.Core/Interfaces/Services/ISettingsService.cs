namespace HMS.Payments.Core.Interfaces.Services
{
    public interface ISettingsService
    {
        void SetCustomSettings(string json);
        void UpdateSettings(string json);
        Task<string> GetCurrentSettingsAsync();
    }
}
