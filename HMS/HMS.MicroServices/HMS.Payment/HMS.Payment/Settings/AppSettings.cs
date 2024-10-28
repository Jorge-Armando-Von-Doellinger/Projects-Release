using HMS.Payments.Infrastructure.Settings;
using System.Text.Json;

namespace HMS.Payments.API.Settings
{
    public sealed class AppSettings
    {
        public AppSettings(InfratructureSettings infratructureSettings)
        {
            // Objetivo 1: fazer as camadas não quebrarem a api quando as settings não forem validas!
            // Objetivo 2: fazer o set dinamico das configurações da api
            // Objetivo final: conseguir atualizar as configurações sem quebrar a api
        }

        public InfratructureSettings InfratructureSettings { get; private set; }

        internal void UpdateSettings(string json)
        {
            var settings = JsonSerializer.Deserialize<AppSettings>(json);
            InfratructureSettings = settings.InfratructureSettings;
        }
    }
}
