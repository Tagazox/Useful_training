using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.ViewModels;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;

namespace Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get;

public class SearchNeuralNetworkByNameUseCase
{
    private readonly INeuralNetworkWarehouse NeuralNetworkWarehouse;

    public SearchNeuralNetworkByNameUseCase(INeuralNetworkWarehouse neuralNetworkWarehouse)
    {
        NeuralNetworkWarehouse = neuralNetworkWarehouse;
    }

    public NeuralNetworksFoundViewModel Execute(string? like, int start = 0, int count = 10)
    {
        return new NeuralNetworksFoundViewModel(NeuralNetworkWarehouse.SearchAvailable(like, start, count)
            .Select(p => Path.GetFileNameWithoutExtension(p) ?? ""));
    }
}