using System.Text.Json;

namespace HMS.ContractsMicroService.API.Services
{
    public sealed class AppSettingsService
    {
        public AppSettingsService()
        {
            
        }
        internal JsonElement TryGetJsonElemt(IConfigurationSection section)
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

        internal JsonElement GetSettings(IConfiguration configuration)
        {
            var section = configuration.GetSection("DefaultAppSettings");
            var jsonElement = TryGetJsonElemt(section);
            return jsonElement;
        }

    }
}
