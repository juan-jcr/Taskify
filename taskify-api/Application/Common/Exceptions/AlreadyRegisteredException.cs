namespace Application.Common.Exceptions;

public class AlreadyRegisteredException : Exception
{
    public AlreadyRegisteredException(string message): base(message) { }
}