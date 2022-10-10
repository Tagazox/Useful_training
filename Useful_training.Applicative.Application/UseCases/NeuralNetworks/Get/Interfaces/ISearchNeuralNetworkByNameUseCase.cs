using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.ViewModels;

namespace Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.Interfaces;

public interface ISearchNeuralNetworkByNameUseCase
{
    public NeuralNetworksFoundViewModel Execute(string? like, int start = 0, int count = 10);
}