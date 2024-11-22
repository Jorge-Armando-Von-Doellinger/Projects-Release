using HMS.Payments.Core.Exceptions;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Context
{
    public sealed class RabbitContext
    {
        private readonly IOptionsMonitor<MessagingSettings> _settings;


        public RabbitContext(IOptionsMonitor<MessagingSettings> settings)
        {
            _settings = settings;
        }
        internal async Task ConfigureChannel(IChannel model)
        {
            await ConfigurePaymentsEmployeeChannel(model);
            await ConfigurePaymentsChannel(model);
        }

        // Methods configuration
        private async Task ConfigurePaymentsChannel(IChannel model)
        {
            await Configure(model, _settings.CurrentValue);
        }
        private async Task ConfigurePaymentsEmployeeChannel(IChannel model)
        {
            await Configure(model, _settings.CurrentValue);
        }
        private async Task Configure(IChannel model, MessagingSettings settings)
        {
            if (settings == null) throw new SettingsNullException("Messaging settings cannot be null");
            await model.ExchangeDeclareAsync(settings.Exchange, settings.TypeExchange, true, false);
            foreach (var queue in settings.Queues.Values)
            {
                await model.QueueDeclareAsync(queue, true, true, false);
                await model.QueueBindAsync(queue, settings.Exchange, string.Empty);
            }
        }
    }
}
