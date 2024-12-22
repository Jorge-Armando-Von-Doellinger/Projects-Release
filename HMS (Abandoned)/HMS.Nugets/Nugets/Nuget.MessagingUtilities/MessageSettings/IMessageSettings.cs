namespace Nuget.MessagingUtilities.MessageSettings
{
    public interface IMessageSettings
    {
        public string Exchange { get; }
        public string TypeExchange { get; }
        public string Queue { get; }
        public string BaseRoutingKey { get; }
        public string AddKey { get; }
        public string UpdateKey { get; }
        public string? GetKey { get; }
        public string DeleteKey { get; }
        public string? GetByIdKey { get; }
        public string ResponseBase { get; }
        public string CurrentKey { get; set; }
    }
}
