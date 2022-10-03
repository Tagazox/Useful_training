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
    public class NeuralNetworkTrainer : INeuralNetworkTrainer
    {
        private readonly INeuralNetwork neural_Network;
        private readonly List<DataSet> dataSets;
        private readonly int _roundedTo;
        private List<INeuralNetworkTrainerObserver> Observers;
        public NeuralNetworkTrainer(INeuralNetworkTrainerContainer neuralNetworkContainer, int roundedTo = 3)
        {
            _roundedTo = roundedTo;
            neuralNetworkContainer.CreateNeuralNetwork();
            neural_Network = neuralNetworkContainer.Neural_Network;
            neuralNetworkContainer.CreateDataSets();
            dataSets = neuralNetworkContainer.DataSets;

            Observers = new List<INeuralNetworkTrainerObserver>();

            if (neural_Network == null)
                throw new NullReferenceException("Neural_Network hasn't be find in the container");
            if (dataSets == null)
                throw new NullReferenceException("Data set hasn't be find in the container");
        }

        public void TrainNeuralNetwork()
        {
            bool trainFinish = false;
            Random random = new Random();

            while (!trainFinish)
            {
                DataSet dataSetForThisIteration = dataSets[random.Next(dataSets.Count)];

                IList<double> resultsOfTheNeuralNetworkCalculation = neural_Network.Calculate(dataSetForThisIteration.Inputs);
                if (resultsOfTheNeuralNetworkCalculation.Any(d => double.IsNaN(d)))
                    neural_Network.Reset();
                else
                {
                    Notify(new NeuralNetworkObservableData(dataSetForThisIteration, resultsOfTheNeuralNetworkCalculation));
                    if (!trainFinish)
                        neural_Network.BackPropagate(dataSetForThisIteration.TargetOutput);
                }


                if (CalculateError(dataSetForThisIteration.TargetOutput, resultsOfTheNeuralNetworkCalculation) < 0.001)
                    trainFinish = VerifyIfTrainingIsFinish();
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
            foreach (DataSet set in dataSets.Take(20))
            {
                IList<double> results = neural_Network.Calculate(set.Inputs);
                if (results.Any(d => double.IsNaN(d)))
                {
                    neural_Network.Reset();
                    return false;
                }
                for (int i = 0; i < results.Count; i++)
                    if (Math.Round(Math.Abs(results[i] - set.TargetOutput[i]), _roundedTo) > 0.001)
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
