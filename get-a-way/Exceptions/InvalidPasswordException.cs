namespace get_a_way.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base("Invalid password.")
    {
    }

    public InvalidPasswordException(string details) 
        : base($"Invalid password. {details}")
    {
    }
}
