namespace Core.Entities.Exceptions;
public class InsufficientAlternativesException : Exception
{
    public InsufficientAlternativesException(string message) : base(message) {}
}
