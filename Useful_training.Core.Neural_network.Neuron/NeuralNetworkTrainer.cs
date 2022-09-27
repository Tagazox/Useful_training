using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network
{
    public class NeuralNetworkTrainer : INeuralNetworkTrainer
    {
        private readonly INeural_Network _neural_Network;
        private readonly List<DataSet> _dataSets;
        public NeuralNetworkTrainer(INeuralNetworkTrainerContainer neuralNetworkContainer)
        {
            neuralNetworkContainer.CreateNeuralNetwork();
            _neural_Network = neuralNetworkContainer.Neural_Network;
            neuralNetworkContainer.CreateDataSets();
            _dataSets = neuralNetworkContainer.DataSets;
        }

        public void TrainNeuralNetwork()
        {
            double deltaError = double.MaxValue;
            bool trainFinish = false;
            Random random = new Random();
            IList<double> results;
            var timer = new Stopwatch();
            timer.Start();
            if (_neural_Network == null)
                throw new NullReferenceException("Data set hasn't be fine in the container");
            if (_dataSets == null)
                throw new NullReferenceException("Data set hasn't be fine in the container");
            while (!trainFinish)
            {
                deltaError = 0;
                trainFinish = false;
                DataSet dataSet = _dataSets[random.Next(_dataSets.Count)];
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
                    foreach (DataSet set in _dataSets.Take(20))
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
            }
        }
    }
}
