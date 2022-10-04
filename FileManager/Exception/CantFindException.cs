namespace Useful_training.Infrastructure.FileManager.Exceptions
{
    public class CantFindException : Exception
    {
            public CantFindException(string message) : base(message)
            {
            }
    }
}
