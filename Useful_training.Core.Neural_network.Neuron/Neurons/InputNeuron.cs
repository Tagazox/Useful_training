using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons;

internal class InputNeuron : IInputNeuron
{
	public double OutputResult { get; set; }
	List<Synapse> IInputNeuron.OutputSynapses { get; } = new List<Synapse>();
	public IInputNeuron Clone()
	{
		return (IInputNeuron)MemberwiseClone();
	}
}