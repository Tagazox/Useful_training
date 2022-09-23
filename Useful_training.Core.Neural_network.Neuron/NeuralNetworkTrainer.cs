using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network
{
    public class NeuralNetworkTrainer
    {
        Neural_Network _neural_Network;
        public NeuralNetworkTrainer(INeuralNetworkContainer neuralNetworkContainer)
        {
            _neural_Network = neuralNetworkContainer.GetNeuralNetwork();
        }

        public Neural_Network GetTrainedNeuralNetwork(List<DataSet> dataSets)
        {
            double deltaError = double.MaxValue;
            int trainFinish = 10;
            Random random = new Random();
            IList<double> results;
            while (trainFinish !=0)
            {
                DataSet dataSet = dataSets[random.Next(dataSets.Count)];
                results = _neural_Network.Calculate(dataSet.Values);
                if (results.Count != dataSet.Targets.Count)
                    throw new ArgumentException("targetOutputsValues need to have the same number as the neurones outputs");
                for (int i = 0; i < results.Count; i++)
                {
                    deltaError = results[i] - dataSet.Targets[i];
                    if (Math.Abs(deltaError) > 0.00001)
                    {
                        trainFinish = 10;
                    }
                    else
                        trainFinish--;
                }
                _neural_Network.BackPropagate(dataSet.Targets);
                if (results.Any(d => double.IsNaN(d)) )
                    return null;
            }
            return _neural_Network;
        }
    }
}
