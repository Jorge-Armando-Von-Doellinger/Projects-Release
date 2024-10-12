namespace HMS.ContractsMicroService.Messaging.Exceptions
{
    public sealed class AttemptLimitException : Exception
    {
        public AttemptLimitException(string Message) : base(Message) 
        {
            
        }
    }
}
