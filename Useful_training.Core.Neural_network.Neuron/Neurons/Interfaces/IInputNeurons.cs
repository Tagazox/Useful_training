using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests.Test")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Interfaces;

internal interface IInputNeuron
{
	public double OutputResult { get; set; }
	internal List<Synapse> OutputSynapses { get; }
	// ReSharper disable once UnusedMemberInSuper.Global
	public IInputNeuron Clone();
}