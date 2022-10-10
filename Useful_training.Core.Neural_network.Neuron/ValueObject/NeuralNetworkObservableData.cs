﻿using Useful_training.Core.NeuralNetwork.Observer.Interfaces;

namespace Useful_training.Core.NeuralNetwork.ValueObject;

[Serializable]
public class NeuralNetworkObservableData : INeuralNetworkObservableData
{
	public DataSet DataSet { get; }
	public IList<double> Results { get; }
	public IEnumerable<double> DeltasErrors { get
		{
			double[] deltaErrors = new double[Results.Count];
			if (Results.Count != DataSet.TargetOutput.Count)
				throw new Exception("Results is not equal to the targets, can't calculate the deltas.");
			for (int i = 0; i < Results.Count; i++)
				deltaErrors[i] = DataSet.TargetOutput[i]- Results[i];
			return deltaErrors;
		}
	}
	public NeuralNetworkObservableData(DataSet dataSet, IList<double> results)
	{
		DataSet = dataSet;
		Results = results;
	}
}