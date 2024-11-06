namespace HMS.Payments.Application.Exceptions
{
    internal sealed class InvalidMessageException : Exception
    {
        public InvalidMessageException(string message) : base(message)
        { }
    }
}
