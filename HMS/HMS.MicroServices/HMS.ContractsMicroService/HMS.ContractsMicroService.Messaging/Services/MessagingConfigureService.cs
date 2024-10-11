using Nuget.Settings.Messaging;
using RabbitMQ.Client;
using System.Reflection;

namespace HMS.ContractsMicroService.Messaging.Services
{
    internal sealed class MessagingConfigureService
    {
        internal static Task ConfigureQueue(IModel model, RabbitMqSettings settings, bool? setAllKeys)
        {
            if(setAllKeys == true)
                QueueBindAllKeys(settings, model);
            else
                model.QueueBind(settings.Queue, settings.Exchange, settings.CurrentKey);
            return Task.CompletedTask;
        }

        private static void QueueBindAllKeys(RabbitMqSettings settings, IModel model)
        {
            foreach(var key in settings.GetKeys())
            {
                model.QueueBind(settings.Queue, settings.Exchange, key);
            }
        }
    }
}
