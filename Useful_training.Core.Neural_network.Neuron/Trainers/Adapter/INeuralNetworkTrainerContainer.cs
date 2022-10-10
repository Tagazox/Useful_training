using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.Trainers.Adapter;

public interface INeuralNetworkTrainerContainer
{
    List<DataSet> DataSets { get; }
    INeuralNetwork NeuralNetwork{ get; }

}