namespace HMS.ContractsMicroService.Infrastructure.Exceptions
{
    internal class InvalidSettingsException : Exception
    {
        public InvalidSettingsException(string message) : base(message)
        { }
    }
}
