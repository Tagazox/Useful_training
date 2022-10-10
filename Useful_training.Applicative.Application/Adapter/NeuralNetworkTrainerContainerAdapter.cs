using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.Application.Adapter;

public class NeuralNetworkTrainerContainerAdapter : INeuralNetworkTrainerContainer
{
    public List<DataSet> DataSets { get; }

    public INeuralNetwork NeuralNetwork { get; }

    public NeuralNetworkTrainerContainerAdapter(List<DataSet> dataSets, INeuralNetwork neuralNetwork)
    {
        DataSets = dataSets;
        NeuralNetwork = neuralNetwork;
    }
}