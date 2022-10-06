using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
{
	internal class InputNeuron : IInputNeurons
	{
		public double OutputResult { get; set; }
		List<Synapse> IInputNeurons.OutputSynapses { get; set; } = new List<Synapse>();
		public IInputNeurons Clone()
		{
			return (IInputNeurons)this.MemberwiseClone();
		}
	}
}
