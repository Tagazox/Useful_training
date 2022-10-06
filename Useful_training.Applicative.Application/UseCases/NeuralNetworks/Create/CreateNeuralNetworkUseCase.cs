using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.Factory.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;

namespace Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create;

public class CreateNeuralNetworkUseCase
{
    private readonly INeuralNetworkWarehouse NeuralNetworkWarehouse;
    private readonly INeuralNetworkBuilder BuilderOfNeuralNetwork;
    public CreateNeuralNetworkUseCase(INeuralNetworkWarehouse neuralNetworkWarehouse, INeuralNetworkBuilder builderOfNeuralNetwork)
    {
        NeuralNetworkWarehouse = neuralNetworkWarehouse;
        BuilderOfNeuralNetwork = builderOfNeuralNetwork;
    }
    public async Task<NeuralNetworkCreatedViewModel> ExecuteAsync(string name, uint numberOfInput, uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronByHiddenLayer, double learnRate, double momentum, NeuronType typeOfNeuron)
    {
        NeuralNetworkDirector neuralNetworkDirector = new NeuralNetworkDirector();
        neuralNetworkDirector.NetworkBuilder = BuilderOfNeuralNetwork;
        neuralNetworkDirector.BuildComplexeNeuralNetwork(numberOfInput, learnRate, momentum, numberOfOutputs, numberOfHiddenLayers, numberOfNeuronByHiddenLayer, typeOfNeuron);

        string finalName = $"{name}_Input-{numberOfInput}_Output-{numberOfOutputs}";
        await NeuralNetworkWarehouse.Save(BuilderOfNeuralNetwork.GetNeuralNetwork(), finalName);
        return new NeuralNetworkCreatedViewModel(finalName);
    }
}