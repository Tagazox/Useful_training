using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Interfaces;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
{
	internal class InputNeuron : IInputNeurons
	{
		public double OutputResult { get; set; }
		public List<Synapse> OutputSynapses { get; set; }
		public InputNeuron()
		{
			OutputSynapses = new List<Synapse>();
		}
		public IInputNeurons Clone()
		{
			return (IInputNeurons)this.MemberwiseClone();
		}
	}
}
