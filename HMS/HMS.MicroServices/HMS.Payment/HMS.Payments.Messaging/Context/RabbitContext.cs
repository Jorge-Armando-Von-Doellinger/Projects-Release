using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Context
{
    public sealed class RabbitContext
    {
        private readonly MessagingSystem _messagingSystem;

        public RabbitContext(IOptionsMonitor<MessagingSystem> messagingSystem)
        {
            _messagingSystem = messagingSystem.CurrentValue;

        }
        internal void ConfigureChannel(IModel model)
        {
            ConfigurePaymentsEmployeeChannel(model);
            ConfigurePaymentsChannel(model);
        }

        // Methods configuration
        private void ConfigurePaymentsChannel(IModel model)
        {
            Configure(model, _messagingSystem.GetPaymentComponent());
        }
        private void ConfigurePaymentsEmployeeChannel(IModel model)
        {
            Configure(model, _messagingSystem.GetPaymentEmployeeComponent());
        }
        private void Configure(IModel model, MessagingComponents component)
        {
            if (component == null) throw new KeyNotFoundException("payments-components KEY not founded");
            model.ExchangeDeclare(component.Exchange, component.TypeExchange, true, false);
            model.QueueDeclare(component.Queue, true, false, false);
            model.QueueBind(component.Queue, component.Exchange, component.AllKeys);
        }
    }
}
