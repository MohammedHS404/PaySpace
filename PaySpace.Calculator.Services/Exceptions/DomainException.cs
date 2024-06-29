namespace PaySpace.Calculator.Services.Exceptions;

public sealed class DomainException : InvalidOperationException
{
    public DomainException(string message) : base(message)
    {
    }
}