using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.ValueObject;

namespace Useful_training.Core.Neural_network.Interface
{
	public interface INeuralNetworkObservableData
	{
		DataSet DataSet { get; }
		IList<double> Results { get; }
		double[] DeltasErrors { get; }

	}
}
