namespace Business.Validation;

public class NetworkException : Exception
{
    public NetworkException(string message)
        : base(message)
    {
    }
}