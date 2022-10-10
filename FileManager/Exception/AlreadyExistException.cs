namespace Useful_training.Infrastructure.FileManager.Exception;

public class AlreadyExistException : System.Exception
{
    public AlreadyExistException(string message) : base(message)
    {
    }
}