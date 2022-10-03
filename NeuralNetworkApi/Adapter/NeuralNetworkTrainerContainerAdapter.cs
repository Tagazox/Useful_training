using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.ValueObject;

namespace NeuralNetworkApi.Adapter
{
    public class NeuralNetworkTrainerContainerAdapter : INeuralNetworkTrainerContainer
    {
        public List<DataSet> DataSets { get; set; }

        public INeuralNetwork Neural_Network { get; set; }

        public void CreateDataSets()
        {
        }

        public void CreateNeuralNetwork()
        {
        }
    }
}
