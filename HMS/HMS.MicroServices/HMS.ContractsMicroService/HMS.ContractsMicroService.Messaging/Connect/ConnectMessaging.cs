using HMS.ContractsMicroService.Messaging.Services;
using Nuget.Settings.Messaging;
using RabbitMQ.Client;

namespace HMS.ContractsMicroService.Messaging.Connect
{
    public sealed class ConnectMessaging
    {
        private readonly CacheSettingsService _cacheService;

        public ConnectMessaging(CacheSettingsService cacheService)
        {
            _cacheService = cacheService;
        }

        internal async Task<IModel> Connect()
        {
            var settings = _cacheService.GetMessagingSettings();
            if(settings == null)
            {
                var settingsTask = Task.Run(async () =>
                {
                    while (_cacheService.GetMessagingSettings() == null)
                        await Task.Delay(10);
                });
                var timeout = Task.Delay(3000);
                if (await Task.WhenAny(settingsTask, timeout) == timeout)
                    throw new TimeoutException("Não foi possivel encontrar as configurações em cache");
            }
            var factory = new ConnectionFactory()
            {
                HostName = settings.HostName,
                Port = settings.Port       
            };
            var connection = factory.CreateConnection();
            return connection.CreateModel();
        }
    }
}
