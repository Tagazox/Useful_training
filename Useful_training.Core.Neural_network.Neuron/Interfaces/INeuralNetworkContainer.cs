using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.Interfaces
{
    public interface INeuralNetworkTrainerContainer
    {
        List<DataSet> DataSets { get; set; }
        INeuralNetwork NeuralNetwork{ get; set; }
        public void CreateNeuralNetwork();
        public void CreateDataSets();
    }
}
