using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network.ValueObject
{
	public class NeuralNetworkObservableData : INeuralNetworkObservableData
	{
		public DataSet DataSet { get; }
		public IList<double> Results { get; }
		public double[] DeltasErrors { get
			{
				double[] deltaErrors = new double[Results.Count];
				if (Results.Count != DataSet.Targets.Count)
					throw new Exception("Result is not equal to the targets, can't calculate the deltas.");
				for (int i = 0; i < Results.Count; i++)
					deltaErrors[i] = (DataSet.Targets[i]- Results[i]);
				return deltaErrors;
			}
		}

		public NeuralNetworkObservableData(DataSet dataSet, IList<double> results)
		{
			DataSet = dataSet;
			Results = results;
		}
	}
}
