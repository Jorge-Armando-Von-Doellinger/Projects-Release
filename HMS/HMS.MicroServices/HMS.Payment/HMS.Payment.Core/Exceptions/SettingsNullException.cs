namespace HMS.Payments.Core.Exceptions;

public sealed class SettingsNullException : Exception
{
    public SettingsNullException(string message) : base(message)
    {
        
    }
}