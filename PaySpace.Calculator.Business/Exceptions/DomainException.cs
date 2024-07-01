namespace PaySpace.Calculator.Business.Exceptions;

public sealed class DomainException : InvalidOperationException
{
    public DomainException(string message) : base(message)
    {
    }
}