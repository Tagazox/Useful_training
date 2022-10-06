using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.NeuralNetworkApi.Adapter
{
    public class NeuralNetworkTrainerContainerAdapter : INeuralNetworkTrainerContainer
    {
        public List<DataSet> DataSets { get; set; }

        public INeuralNetwork NeuralNetwork { get; set; }

        public void CreateDataSets()
        {
        }

        public void CreateNeuralNetwork()
        {
        }
    }
}
