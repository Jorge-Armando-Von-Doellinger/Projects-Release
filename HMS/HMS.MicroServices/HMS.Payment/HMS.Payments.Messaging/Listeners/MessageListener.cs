using HMS.Payments.Core.Interfaces.Messaging;
using HMS.Payments.Messaging.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HMS.Payments.Messaging.Listeners
{
    public sealed class MessageListener : IMessageListener
    {
        private IModel _channel;
        private MessagingSystem _messagingSystem;

        public MessageListener(IModel channel, IOptionsMonitor<MessagingSystem> messagingSystem)
        {
            _channel = channel;
            _messagingSystem = messagingSystem.CurrentValue;
        }

        public async Task ListeningAsync(Func<byte[], Task> action)
        {
            var payment = _messagingSystem.GetPaymentComponent();
            var paymentEmplyoee = _messagingSystem.GetPaymentEmployeeComponent();
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (obj, args) =>
            {
                await Task.Run(() =>Console.WriteLine("awdasd"));
                try
                {
                    await action(args.Body.ToArray());
                    _channel.BasicAck(args.DeliveryTag, false);
                }
                catch
                {
                    _channel.BasicReject(args.DeliveryTag, false);
                    throw;
                }
                
            };
            _channel.BasicConsume(payment.Queue, false, consumer);
            //_channel.BasicConsume(paymentEmplyoee.Queue, false, consumer);
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
