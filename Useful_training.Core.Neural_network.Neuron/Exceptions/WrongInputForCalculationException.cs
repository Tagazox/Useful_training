namespace Useful_training.Core.NeuralNetwork.Exceptions;

public class WrongInputForCalculationException : Exception
{
    public WrongInputForCalculationException(string message) : base(message)
    {
    }
}