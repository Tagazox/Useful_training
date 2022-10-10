namespace Useful_training.Core.NeuralNetwork.Exceptions;

public class NeuronTypeDontExistsException : Exception
{
    public NeuronTypeDontExistsException(string message) : base(message)
    {
    }
}