namespace Useful_training.Infrastructure.FileManager.Exception
{
    public class CantFindException : System.Exception
    {
            public CantFindException(string message) : base(message)
            {
            }
    }
}
