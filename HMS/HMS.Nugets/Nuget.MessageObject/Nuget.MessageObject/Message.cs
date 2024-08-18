namespace Nuget.MessageObject
{
    public class Message
    {
        public Guid ID { get; set; }
        public string Content { get; set; }
        public string Origin { get; set; }
        public List<string> MessageFlow { get; set; } // Servicos em que esta mensagem passou!
        public string Destination { get; set; }

        public virtual void AddMessageFlow(string flow)
        {
            MessageFlow.Add(flow);
        }

    }


}
