using Nuget.MessagingUtilities.MessageSettings;
using RabbitMQ.Client;

namespace Gateway.v1.Messaging.Configurator
{
    public sealed class ChannelConfigurator
    {
        internal async Task Configure(IModel channel, IMessageSettings settings)
        {
            try
            {
                if(settings == null || channel == null)
                    throw new NullReferenceException("Referencias nulas");
                channel.ExchangeDeclare(settings.Exchange,
                    settings.TypeExchange,
                    false,
                    false,
                    null);
                channel.QueueBind(settings.Queue,
                                settings.Exchange,
                                settings.CurrentKey);
                channel.QueueDeclare(settings.Queue, 
                    false, 
                    false, 
                    false,
                    null);
                //await Task.CompletedTask;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro ao configurar canal \n" + ex.Message + "\n \n");
                throw ex;
            }
        }
    }
}
