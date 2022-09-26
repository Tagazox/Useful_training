using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network
{
    public class NeuralNetworkTrainer
    {
        private readonly Neural_Network _neural_Network;
        public NeuralNetworkTrainer(INeuralNetworkContainer neuralNetworkContainer)
        {
            _neural_Network = neuralNetworkContainer.GetNeuralNetwork();
        }

        public Neural_Network GetTrainedNeuralNetwork(List<DataSet> dataSets)
        {
            double deltaError = double.MaxValue;
            bool trainFinish = false;
            Random random = new Random();
            IList<double> results;
            var timer = new Stopwatch();
            timer.Start();

            while (!trainFinish)
            {
                deltaError = 0;
                trainFinish = false;
                DataSet dataSet = dataSets[random.Next(dataSets.Count)];
                results = _neural_Network.Calculate(dataSet.Values);
                if (results.Count != dataSet.Targets.Count)
                    throw new ArgumentException("targetOutputsValues need to have the same number as the neurones outputs");
                for (int i = 0; i < results.Count; i++)
                {
                    deltaError += Math.Abs(results[i] - dataSet.Targets[i]);

                }
                _neural_Network.BackPropagate(dataSet.Targets);
                if (deltaError < 0.001)
                {
                    trainFinish = true;
                    foreach (DataSet set in dataSets.Take(20))
                    {
                        results = _neural_Network.Calculate(set.Values);
                        for (int i = 0; i < results.Count; i++)
                        {
                            if (Math.Abs(results[i] - set.Targets[i]) > 0.001)
                            {
                                trainFinish = false;
                                System.Diagnostics.Debug.WriteLine($"Delta error[{i}]:{deltaError}");
                                break;
                            }

                        }
                    }
                }
                if (results.Any(d => double.IsNaN(d)))
                    return null;
            }
            return _neural_Network;
        }
    }
}
