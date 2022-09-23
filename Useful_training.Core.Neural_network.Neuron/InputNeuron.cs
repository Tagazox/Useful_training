using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Interface;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neuron.Tests")]
namespace Useful_training.Core.Neural_network.Neurons
{
	internal class InputNeuron : IInputNeurons
	{
		public double OutputResult { get ; set; }
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
