namespace Useful_training.Core.NeuralNetwork.Exceptions
{
    public class CantInitializeWithZeroInputException : Exception
    {
            public CantInitializeWithZeroInputException(string message) : base(message)
            {
            }
    }
}
