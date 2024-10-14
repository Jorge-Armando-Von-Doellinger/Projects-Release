using Nuget.Settings.Messaging;
using RabbitMQ.Client;
using System.Reflection;

namespace HMS.ContractsMicroService.Messaging.Services
{
    internal sealed class MessagingConfigureService
    {
        private static bool _asConfigured = false;
        internal static void ConfigureQueue(IModel model, IMessagingSystem settings)
        {
            model.QueueBind(settings.Queue, settings.Exchange, settings.CurrentKey);
        }

        internal static void ConfigureAllQueues(IModel model, Dictionary<string, IMessagingSystem> settings)
        {
            foreach(var value in settings.Values)
                model.QueueBind(value.Queue, value.Exchange, value.CurrentKey);
        }
    }
}
