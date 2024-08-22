namespace Core.Entities.Exceptions;
public class UserLoggedOutException : Exception
{
    public UserLoggedOutException(string message) : base(message) { }
}
