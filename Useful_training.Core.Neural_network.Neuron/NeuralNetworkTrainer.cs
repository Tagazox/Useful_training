using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.ValueObject;

namespace Useful_training.Core.Neural_network
{
	public class NeuralNetworkTrainer : INeuralNetworkTrainer, INeuralNetworkObservable
	{
		private readonly INeural_Network _neural_Network;
		private readonly List<DataSet> _dataSets;
		private List<INeuralNetworkTrainerObserver> Observers;
		public NeuralNetworkTrainer(INeuralNetworkTrainerContainer neuralNetworkContainer)
		{
			neuralNetworkContainer.CreateNeuralNetwork();
			_neural_Network = neuralNetworkContainer.Neural_Network;
			neuralNetworkContainer.CreateDataSets();
			_dataSets = neuralNetworkContainer.DataSets;

			Observers = new List<INeuralNetworkTrainerObserver>();

			if (_neural_Network == null)
				throw new NullReferenceException("Neural_Network hasn't be find in the container");
			if (_dataSets == null)
				throw new NullReferenceException("Data set hasn't be find in the container");
		}

		public void TrainNeuralNetwork()
		{
			bool trainFinish = false;
			Random random = new Random();

			while (!trainFinish)
			{
				DataSet dataSetForThisIteration = _dataSets[random.Next(_dataSets.Count)];

				IList<double> resultsOfTheNeuralNetworkCalculation = _neural_Network.Calculate(dataSetForThisIteration.Values);

				if (resultsOfTheNeuralNetworkCalculation.Count != dataSetForThisIteration.Targets.Count)
					throw new ArgumentException("The target of the dataset need to have the same number as the neurones outputs");

				Notify(new NeuralNetworkObservableData(dataSetForThisIteration, resultsOfTheNeuralNetworkCalculation));
				
				if (CalculateError(dataSetForThisIteration.Targets, resultsOfTheNeuralNetworkCalculation) < 0.001)
					trainFinish = VerifyIfTrainingIsFinish();

				if (!trainFinish)
					_neural_Network.BackPropagate(dataSetForThisIteration.Targets);
			}
		}

		private double CalculateError(List<double> targets, IList<double> results)
		{
			double deltaError = 0;
			for (int i = 0; i < results.Count; i++)
				deltaError += Math.Abs(results[i] - targets[i]);
			return deltaError;
		}

		private bool VerifyIfTrainingIsFinish()
		{
			foreach (DataSet set in _dataSets.Take(20))
			{
				IList<double> results = _neural_Network.Calculate(set.Values);
				for (int i = 0; i < results.Count; i++)
					if (Math.Abs(results[i] - set.Targets[i]) > 0.001)
					{
						return false;
					}
			}
			return true;
		}

		public void Attach(INeuralNetworkTrainerObserver observer)
		{
			Observers.Add(observer);
		}

		public void Detach(INeuralNetworkTrainerObserver observer)
		{
			Observers.Add(observer);
		}

		public void Notify(INeuralNetworkObservableData datas)
		{
			foreach (INeuralNetworkTrainerObserver observer in Observers)
				observer.Update(datas);
		}
	}
}
