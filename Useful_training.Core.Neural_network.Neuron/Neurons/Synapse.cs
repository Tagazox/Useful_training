using Useful_training.Core.NeuralNetwork.Helpers;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;

namespace Useful_training.Core.NeuralNetwork.Neurons;

internal class Synapse
{
    public IInputNeuron InputNeuron { get; }
    public INeuron OutputNeuron { get; }
    public double Weight { get; set; }
    public double WeightDelta { get; set; }

    public Synapse(IInputNeuron inputNeuron, INeuron outputNeuron)
    {
        InputNeuron = inputNeuron;
        OutputNeuron = outputNeuron;
        Weight = RandomNumber.GetMirrorDouble();
        WeightDelta = RandomNumber.GetMirrorDouble();
    }

    internal void Reset()
    {
        Weight = RandomNumber.GetMirrorDouble();
        WeightDelta = RandomNumber.GetMirrorDouble();
    }
}