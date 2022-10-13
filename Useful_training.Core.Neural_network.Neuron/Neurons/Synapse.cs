using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;

namespace Useful_training.Core.NeuralNetwork.Neurons;

internal class Synapse
{
    private static readonly Random Rand = new Random();
    public IInputNeuron InputNeuron { get; }
    public INeuron OutputNeuron { get; }
    public double Weight { get; set; }
    public double WeightDelta { get; set; }
    public Synapse(IInputNeuron inputNeuron, INeuron outputNeuron)
    {
        Random rand = new Random();
        InputNeuron = inputNeuron;
        OutputNeuron = outputNeuron;
        Weight = rand.NextDouble() * 2 - 1;
        WeightDelta =rand.NextDouble() * 2 - 1;

    }
    internal void Reset()
    {
        Weight = Rand.NextDouble() * 2 - 1;
        WeightDelta =Rand.NextDouble() * 2 - 1;
    }
}