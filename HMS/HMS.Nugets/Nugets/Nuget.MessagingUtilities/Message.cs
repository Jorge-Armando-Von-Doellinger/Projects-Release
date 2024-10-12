using System.Text.Json.Serialization;

namespace Nuget.MessagingUtilities
{
    public class Message
    {
        [JsonConstructor]
        public Message(object content, string origin, string destination)
        {
            Content = content;
            Origin = origin;
            Destination = destination;
        }
        [JsonInclude]
        public Guid ID { get; } = Guid.NewGuid();
        [JsonInclude]
        public byte RetryCount { get; private set; } = 0;
        public object Content { get; set; }
        

        public string Origin { get; }
        public List<string> MessageFlow { get; private set; } = new(); // Servicos em que esta mensagem passou! Ex: gateway, client.add, response

        public string Destination { get; }    // Ex: client.add
        public void AddAttempt()
        {
            RetryCount++;
        }
        public virtual void AddMessageFlow(string flow)
        {
            if(flow != null)
                MessageFlow.Add(flow);
        }
    }
}
