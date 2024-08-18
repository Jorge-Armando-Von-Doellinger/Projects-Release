namespace Nuget.Communication.Messaging.MessageSettings
{
    public static class ResponseSettings
    {
        public static string Queue { get; } = "Response";
        public static string Exchange { get; } = "Response";
        public static string BaseRoutingKey { get; } = "Response.";
        public static string CreateResponseKey(string responseBase)
        {
            if(responseBase.ToCharArray().Select(x => x == '.').Count() > 0)
            {

                return BaseRoutingKey + responseBase;
            }
            return null;
        }
    }
}
