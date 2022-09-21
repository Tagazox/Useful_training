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

        public Neural_Network GetTrainedNeuralNetwork(List<double> inputSample,List<double> targetOutputsValues)
        {
            double deltaError = double.MaxValue;
            bool trainFinish = false;   
            IList<double> results;
            while (!trainFinish)
            {
                trainFinish = true;

                results = _neural_Network.Calculate(inputSample);
                if (results.Count != targetOutputsValues.Count)
                    throw new ArgumentException("targetOutputsValues need to have the same number as the neurones outputs");
                for (int i = 0; i < results.Count; i++)
                {
                    deltaError = results[i] - targetOutputsValues[i];
                    if (Math.Abs(deltaError) > 0.00001 && trainFinish)
                    {
                        trainFinish = false;
                    }
                }

                
                _neural_Network.BackPropagate(targetOutputsValues);
                if (double.IsNaN(results.First()))
                    throw new Exception("Corrupted network");
            }
            return _neural_Network;
        }
    }
}
