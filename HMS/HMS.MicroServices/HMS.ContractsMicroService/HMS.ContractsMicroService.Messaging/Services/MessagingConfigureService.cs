using HMS.ContractsMicroService.Messaging.Settings.Interfaces;
using RabbitMQ.Client;

namespace HMS.ContractsMicroService.Messaging.Services
{
    internal sealed class MessagingConfigureService
    {
        private static bool _asConfigured = false;
        internal static void ConfigureQueue(IModel model, IMessagingComponents settings)
        {
            model.QueueBind(settings.Queue, settings.Exchange, settings.CurrentKey);
        }

        internal static void ConfigureAllQueues(IModel model, IMessagingSystem settings)
        {
            foreach(var value in settings.Components.Values)
                model.QueueBind(value.Queue, value.Exchange, "contract.#");
        }
    }
}
