namespace HMS.ContractsMicroService.Messaging.Exceptions
{
    internal class InvalidMessageException : Exception
    {
        public InvalidMessageException(string message) : base(message)
        { }
    }
}
