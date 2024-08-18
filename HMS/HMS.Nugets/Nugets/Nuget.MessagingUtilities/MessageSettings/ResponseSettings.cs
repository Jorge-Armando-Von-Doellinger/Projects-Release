namespace Nuget.MessagingUtilities.MessageSettings
{
    public static class ResponseSettings
    {
        public static string Queue { get; } = "Response";
        public static string Exchange { get; } = "Response";
        public static string ExchangeType { get; } = "direct";
        public static string BaseResponseKey { get; } = "Response.";
    }
}
