using Useful_training.Core.NeuralNetwork.Factory.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.Factory;

public class NeuralNetworkBuilder : INeuralNetworkBuilder
{
    private NeuralNetwork.NeuralNetwork _neuralNetwork = new NeuralNetwork.NeuralNetwork();

    public void Reset()
    {
        _neuralNetwork = new NeuralNetwork.NeuralNetwork();
    }

    public void Initialize(uint numberOfInput, double? learnRate = null, double? momentum = null)
    {
        _neuralNetwork.Initialize(numberOfInput, learnRate, momentum);
    }

    public void AddHiddenLayers(uint numberOfNeuronesByLayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons)
    {
        for (int i = 0; i < numberOfHiddenLayers; i++)
            _neuralNetwork.AddHiddenLayer(numberOfNeuronesByLayers, typeOfNeurons);
    }

    public void AddHiddenLayers(List<uint> numberOfNeuronesByLayer, uint numberOfHiddenLayers,
        NeuronType typeOfNeurons)
    {
        if (numberOfNeuronesByLayer.Count != numberOfHiddenLayers)
            throw new ArgumentException(
                "count of numberOfNeuronesByLayers arrays need to be equal as the number of hidden layers.");

        for (int i = 0; i < numberOfHiddenLayers; i++)
            _neuralNetwork.AddHiddenLayer(numberOfNeuronesByLayer[i], typeOfNeurons);
    }

    public void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons)
    {
        if (numberOfOutputs <= 0)
            throw new ArgumentException("You can't build a neural network without any outputs.");
        _neuralNetwork.AddHiddenLayer(numberOfOutputs, typeOfNeurons);
    }

    public NeuralNetwork.NeuralNetwork GetNeuralNetwork()
    {
        return _neuralNetwork;
    }
}