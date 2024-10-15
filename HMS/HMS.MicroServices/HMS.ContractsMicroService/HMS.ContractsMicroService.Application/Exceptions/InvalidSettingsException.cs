namespace HMS.ContractsMicroService.Application.Exceptions
{
    public sealed class InvalidSettingsException : Exception
    {
        public InvalidSettingsException(string message) : base(message) 
        {
            
        }
    }
}
