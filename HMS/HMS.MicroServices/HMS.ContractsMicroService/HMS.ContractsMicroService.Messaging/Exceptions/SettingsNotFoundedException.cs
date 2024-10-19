namespace HMS.ContractsMicroService.Messaging.Exceptions
{
    internal sealed class SettingsNotFoundedException : Exception
    {
        public SettingsNotFoundedException(string message) : base(message)
        {

        }
    }
}
