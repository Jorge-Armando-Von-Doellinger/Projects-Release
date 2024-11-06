using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Core.Interfaces.Processor;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Channels;

namespace HMS.Payments.Messaging.Listeners
{
    public sealed class MessageListener : IMessageListener
    {
        private readonly Channel<byte[]> _channelThread;
        private IModel _channel;
        private readonly IMessageProcessor _messageProcessor;
        private MessagingSystem _messagingSystem;
        private List<byte[]> data = new();

        public MessageListener(IServiceProvider serviceProvider, IOptionsMonitor<MessagingSystem> messagingSystem, IMessageProcessor messageProcessor)
        {
            _channel = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IModel>();
            _messageProcessor = messageProcessor;
            _messagingSystem = messagingSystem.CurrentValue;
        }

        public async Task ListeningAsync(/*Func<byte[], Task> action*/)
        {
            var payment = _messagingSystem.GetPaymentComponent();
            var paymentEmplyoee = _messagingSystem.GetPaymentEmployeeComponent();
            _channel.BasicQos(0, 200, false);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (obj, args) =>
            {
                data.Add(args.Body.ToArray());
                if (data.Count > 50)
                {
                    List<byte[]> dataCopy = new(data);
                    await ProcessInParalell(dataCopy);
                }
            };
            _channel.BasicConsume(payment.Queue, true, consumer);
            _channel.BasicConsume(paymentEmplyoee.Queue, true, consumer);

        }
        private async Task ProcessInParalell(List<byte[]> dataCopy)
        {
            await _messageProcessor.Process(dataCopy);
        }


        public void ListeningSync(Action<byte[]> action)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (obj, args) =>
            {
                action(args.Body.ToArray());
            };
        }
    }
}
