using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.Factory.Interfaces;

public interface INeuralNetworkBuilder
{
    public void Initialize(uint numberOfInput, double? learnRate = null, double? momentum = null);

    public void AddHiddenLayers(List<uint> numberOfNeuronesByLayers, uint numberOfHiddenLayers,
        NeuronType typeOfNeurons);

    public void AddHiddenLayers(uint numberOfNeuronesByLayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
    public void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons);
    public NeuralNetwork.NeuralNetwork GetNeuralNetwork();
    // ReSharper disable once UnusedMember.Global
    public void Reset();
}