namespace Useful_training.Core.NeuralNetwork.Exceptions;

public class CantInitializeWithZeroNeuronException : Exception
{
    public CantInitializeWithZeroNeuronException(string message) : base(message)
    {
    }
}