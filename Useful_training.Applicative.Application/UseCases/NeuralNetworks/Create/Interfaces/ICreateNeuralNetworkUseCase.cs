using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.ViewModels;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Applicative.Application.UseCases.NeuralNetworks.Create.Interfaces;

public interface ICreateNeuralNetworkUseCase
{
    public Task<NeuralNetworkCreatedViewModel> ExecuteAsync(string name, uint numberOfInput, uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronByHiddenLayer, double learnRate, double momentum, NeuronType typeOfNeuron);
}