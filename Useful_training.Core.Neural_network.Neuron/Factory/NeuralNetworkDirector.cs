using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Factory.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.Factory;

public class NeuralNetworkDirector
{
    private INeuralNetworkBuilder? _networkBuilder;
    public INeuralNetworkBuilder NetworkBuilder
    {
        set => _networkBuilder = value;
    }
    public void BuildMinimalNeuralNetwork(uint numberOfInput, uint numberOfOutputs, NeuronType typeOfNeurons)
    {
        if (_networkBuilder == null)
            throw new BuilderNotDefinedException("Builder need to be defined first");
        _networkBuilder.Initialize(numberOfInput);
        _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
    }

    public void BuildComplexNeuralNetwork(uint numberOfInput, double learnRate, double momentum, uint numberOfOutputs,
        uint numberOfHiddenLayers, uint numberOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
    {
        if (_networkBuilder == null)
            throw new BuilderNotDefinedException("Builder need to be defined first");
        _networkBuilder.Initialize(numberOfInput, learnRate, momentum);
        _networkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayer, numberOfHiddenLayers, typeOfNeurons);
        _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
    }

    public void BuildComplexNeuralNetwork(uint numberOfInput, double learnRate, double momentum, uint numberOfOutputs,
        uint numberOfHiddenLayers, List<uint> numbersOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
    {
        if (_networkBuilder == null)
            throw new BuilderNotDefinedException("Builder need to be defined first");
        _networkBuilder.Initialize(numberOfInput, learnRate, momentum);
        _networkBuilder.AddHiddenLayers(numbersOfNeuronesByHiddenLayer, numberOfHiddenLayers, typeOfNeurons);
        _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
    }
}