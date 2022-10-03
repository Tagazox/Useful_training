
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network
{
    public interface INeuralNetworkTrainer : INeuralNetworkObservable
    {
        void TrainNeuralNetwork();
    }
}