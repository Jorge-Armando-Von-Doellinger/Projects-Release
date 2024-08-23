namespace Gateway.v1.Core.Messaging.Publisher
{
    public interface IMessagePublisher<MessageSettings> // A Configuração virá por injeção de dependencia
    {
        // Na classe que implementará essa interface, será necessario um IMessageSettings
        public Task PublishMessage(object message, MessageSettings settings);
    }
}
