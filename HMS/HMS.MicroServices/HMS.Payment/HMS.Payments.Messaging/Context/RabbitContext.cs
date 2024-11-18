using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Context
{
    public sealed class RabbitContext
    {
        private readonly MessagingSettings _settings;

        public RabbitContext(IOptionsMonitor<MessagingSettings> messagingSystem)
        {
            _settings = messagingSystem.CurrentValue;

        }
        internal void ConfigureChannel(IModel model)
        {
            ConfigurePaymentsEmployeeChannel(model);
            ConfigurePaymentsChannel(model);
        }

        // Methods configuration
        private void ConfigurePaymentsChannel(IModel model)
        {
            Configure(model, _settings);
        }
        private void ConfigurePaymentsEmployeeChannel(IModel model)
        {
            Configure(model, _settings);
        }
        private void Configure(IModel model, MessagingSettings settings)
        {
            if (settings == null) throw new KeyNotFoundException("payments-components KEY not founded");
            model.ExchangeDeclare(settings.Exchange, settings.TypeExchange, true, false);
            foreach (var queue in settings.Queues)
            {
                model.QueueDeclare(queue, true, true, false);
                model.QueueBind(queue, settings.Exchange, string.Empty);
            }
        }
    }
}
