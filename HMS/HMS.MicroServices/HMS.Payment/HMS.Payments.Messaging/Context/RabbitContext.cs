using RabbitMQ.Client;

namespace HMS.Payments.Messaging.Context
{
    public sealed class RabbitContext
    {
        internal void SetQueue(IModel model, string queue, string exchange, string type, string routingKey)
        {
            model.ExchangeDeclare(exchange, type, true, false, null);
            model.QueueDeclare(queue, true, false, false);
            model.QueueBind(queue, exchange, routingKey);
        }
        internal void SetQueue(IModel model)
        {

        }
}
}
