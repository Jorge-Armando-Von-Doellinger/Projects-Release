using Nuget.Settings.Messaging;
using RabbitMQ.Client;
using System.Reflection;

namespace HMS.ContractsMicroService.Messaging.Services
{
    internal sealed class MessagingConfigureService
    {
        private static bool _asConfigured = false;
        internal static Task ConfigureQueue(IModel model, RabbitMqSettings settings, bool? setAllKeys)
        {
            if(_asConfigured) return Task.CompletedTask;
            if(setAllKeys == true)
                QueueBindAllKeys(settings, model);
            else
                model.QueueBind(settings.Queue, settings.Exchange, settings.CurrentKey);
            _asConfigured = true;
            return Task.CompletedTask;
        }

        private static void QueueBindAllKeys(RabbitMqSettings settings, IModel model)
        {
            model.QueueBind(settings.Queue, settings.Exchange, "contract.#");
        }
    }
}
