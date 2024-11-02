namespace HMS.Payments.Application.Exceptions
{
    internal sealed class MessageInvalidException : Exception
    {
        public MessageInvalidException(string message) : base(message) 
        { }
    }
}
