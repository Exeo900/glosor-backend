namespace Core.Entities.Exceptions;
public class DuplicateQuestionException : Exception
{
    public DuplicateQuestionException(string message) : base(message) { }
}
