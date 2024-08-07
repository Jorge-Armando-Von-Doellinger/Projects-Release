namespace Nuget.MessageObject
{
    public abstract class Message
    {
        public virtual Guid ID { get; set; }
        public string Content { get; set; }
        public string Origin { get; set; }
        public List<string> MessageFlow { get; set; } // Servicos em que esta mensagem passou!
        public string Destination { get; set; }
    }
}
