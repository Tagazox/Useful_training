using Useful_training.Applicative.Application.Ports;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.Interfaces;
using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.Factory.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;

namespace Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create;

public class CreateNeuralNetworkUseCase : ICreateNeuralNetworkUseCase
{
    private readonly INeuralNetworkWarehouse _neuralNetworkWarehouse;
    private readonly INeuralNetworkBuilder _builderOfNeuralNetwork;
    public CreateNeuralNetworkUseCase(INeuralNetworkWarehouse neuralNetworkWarehouse, INeuralNetworkBuilder builderOfNeuralNetwork)
    {
        _neuralNetworkWarehouse = neuralNetworkWarehouse;
        _builderOfNeuralNetwork = builderOfNeuralNetwork;
    }
    public async Task<NeuralNetworkCreatedViewModel> ExecuteAsync(string name, uint numberOfInput, uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronByHiddenLayer, double learnRate, double momentum, NeuronType typeOfNeuron)
    {
        NeuralNetworkDirector neuralNetworkDirector = new NeuralNetworkDirector
        {
            NetworkBuilder = _builderOfNeuralNetwork
        };
        neuralNetworkDirector.BuildComplexNeuralNetwork(numberOfInput, learnRate, momentum, numberOfOutputs, numberOfHiddenLayers, numberOfNeuronByHiddenLayer, typeOfNeuron);

        string finalName = $"{name}_Input-{numberOfInput}_Output-{numberOfOutputs}";
        await _neuralNetworkWarehouse.Save(_builderOfNeuralNetwork.GetNeuralNetwork(), finalName);
        return new NeuralNetworkCreatedViewModel(finalName);
    }
}