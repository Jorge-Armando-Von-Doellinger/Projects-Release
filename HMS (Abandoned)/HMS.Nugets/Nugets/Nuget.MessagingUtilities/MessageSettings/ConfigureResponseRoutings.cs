namespace Nuget.MessagingUtilities.MessageSettings
{
    public sealed class ConfigureResponseRoutings
    {
        public ConfigureResponseRoutings()
        {

        }
        public string GetResponseKey(string baseResponse)
        {
            if(baseResponse[0] == '.')
                baseResponse = baseResponse.Substring(1);
            if(baseResponse.ToCharArray().Select(x => x == '.').Count() > 0)
                return ResponseSettings.BaseResponseKey + baseResponse;
            return null;
        }
    }
}
