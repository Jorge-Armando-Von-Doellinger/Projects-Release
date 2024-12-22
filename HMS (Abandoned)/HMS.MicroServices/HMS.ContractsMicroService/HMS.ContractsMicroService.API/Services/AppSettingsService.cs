using HMS.ContractsMicroService.API.Settings.Interfaces;
using HMS.ContractsMicroService.Core.Interfaces.Services;
using System.Text.Json;

namespace HMS.ContractsMicroService.API.Services
{
    public sealed class AppSettingsService
    {
        private readonly ISettingsService? _settingsService;
        private readonly IAppSettings? _appSettings;
        
        public AppSettingsService(ISettingsService settingsService, IAppSettings appSettings)
        {
            _settingsService = settingsService;
            _appSettings = appSettings;
        }
        public AppSettingsService()
        {
            
        }
        private JsonElement TryGetJsonElemt(IConfigurationSection section)
        {
            var dict = new Dictionary<string, object>();

            foreach (var child in section.GetChildren())
            {
                if (child.GetChildren().Any())
                    dict[child.Key] = TryGetJsonElemt(child);
                else
                {
                    try
                    {
                        var value = Convert.ToInt32(child.Value);
                        dict[child.Key] = value;
                        continue;
                    }
                    catch { }
                    dict[child.Key] = child.Value;
                }
            }
            var jsonString = JsonSerializer.Serialize(dict, new JsonSerializerOptions { WriteIndented = true });
            var jsonDoc = JsonDocument.Parse(jsonString);
            return jsonDoc.RootElement;
        }

        internal JsonElement GetDefaultSettings(IConfiguration configuration)
        {
            var section = configuration.GetSection("DefaultAppSettings");
            var jsonElement = TryGetJsonElemt(section);
            return jsonElement;
        }

        internal async Task<JsonElement> GetDigitalSettings()
        {
            if (_appSettings == null || _settingsService == null) throw new InvalidCastException("null");
            var settings =  await _settingsService.GetJsonSettings(_appSettings.ApplicationName);
            var element = JsonSerializer.Deserialize<JsonElement>(settings);
            return element;
        }

    }
}
