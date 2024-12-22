using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using RabbitMQ.Client;

namespace HMS.ContractsMicroService.Messaging.Services
{
    internal sealed class MessagingConfigureService
    {
        private static bool _asConfigured = false;
        internal static void ConfigureQueue(IModel model, IMessagingComponents settings)
        {
            //settings.Key;
            model.QueueBind(settings.Queue, settings.Exchange, "contracts.#");
        }

        internal static void ConfigureAllQueues(IModel model, List<IMessagingComponents> settings)
        {
            foreach (var value in settings)
            {
                ExchangeDeclare(model, value);
                value.Keys.All(x =>
                {
                    model.QueueBind(value.Queue, value.Exchange, x);
                    return true;
                });
            }
        }

        private static void ExchangeDeclare(IModel model, IMessagingComponents components)
        {
            Console.WriteLine(components.Exchange + " Ecchange");
            Console.WriteLine(components.TypeExchange);
            model.ExchangeDeclare(components.Exchange, components.TypeExchange, true, false);
        }
    }
}
