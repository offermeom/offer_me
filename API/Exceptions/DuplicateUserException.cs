namespace API.Exceptions;
public class DuplicateUserException : Exception
{
    public DuplicateUserException() { }
    public DuplicateUserException(string message) : base(message) { }
}