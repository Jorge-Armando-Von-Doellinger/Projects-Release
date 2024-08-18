namespace Nuget.MessagingUtilities
{
    public class Message
    {
        public Guid ID { get; set; }
        public object Content { get; set; }
        public string Origin { get; set; }
        public List<string> MessageFlow { get; set; } // Servicos em que esta mensagem passou! Ex: gateway, client.add, response
        public string Destination { get; set; } // Ex: client.add
        public virtual void AddMessageFlow(string flow)
        {
            MessageFlow.Add(flow);
        }
    }
}
