namespace Nuget.MessagingUtilities
{
    public class Message
    {
        public Message()
        {

        }
        public void Configure(object content, string origin, string destination, string messageFlow = null)
        {
            ID = Guid.NewGuid();
            Content = content;
            Origin = origin;
            Destination = destination;
            AddMessageFlow(messageFlow);
        }
        public Guid ID { get; set; }
        public object Content { get; set; }
        public string Origin { get; set; }
        public List<string> MessageFlow { get; set; } // Servicos em que esta mensagem passou! Ex: gateway, client.add, response
        public string Destination { get; set; } // Ex: client.add
        public virtual void AddMessageFlow(string flow)
        {
            if(MessageFlow == null)
                MessageFlow = new List<string>();
            if(flow != null)
                MessageFlow.Add(flow);
        }
    }
}
