using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.Interface
{

	internal interface IInputNeurons
	{
		double OutputResult { get; set; }
		List<Synapse> OutputSynapses { get; set; }
		public IInputNeurons Clone();


	}
}
