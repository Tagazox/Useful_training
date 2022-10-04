namespace Useful_training.Infrastructure.FileManager.Exceptions
{
    public class AlreadyExistException : Exception
    {
            public AlreadyExistException(string message) : base(message)
            {
            }
    }
}
