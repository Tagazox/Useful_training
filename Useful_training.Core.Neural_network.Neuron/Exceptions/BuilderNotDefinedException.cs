namespace Useful_training.Core.NeuralNetwork.Exceptions;

public class BuilderNotDefinedException : Exception
{
    public BuilderNotDefinedException(string message) : base(message)
    {
    }
}