namespace Useful_training.Core.NeuralNetwork.Helpers;

public class RandomNumber
{
    private static readonly Random Random = new Random();

    public static double GetMirrorDouble(uint maxNumber = 1)
    {
        return (Random.NextDouble() * 2 - 1) * maxNumber;
    }
}