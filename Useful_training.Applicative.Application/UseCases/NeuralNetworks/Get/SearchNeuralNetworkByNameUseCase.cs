using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get.ViewModels;
using Useful_training.Infrastructure.FileManager.Warehouse.Interfaces;

namespace Useful_training.Applicative.Application.UseCases.NeuralNetworks.Get;

public class SearchNeuralNetworkByNameUseCase : ISearchNeuralNetworkByNameUseCase
{
    private readonly INeuralNetworkWarehouse _neuralNetworkWarehouse;

    public SearchNeuralNetworkByNameUseCase(INeuralNetworkWarehouse neuralNetworkWarehouse)
    {
        _neuralNetworkWarehouse = neuralNetworkWarehouse;
    }

    public NeuralNetworksFoundViewModel Execute(string? like, int start = 0, int count = 10)
    {
        return new NeuralNetworksFoundViewModel(_neuralNetworkWarehouse.SearchAvailable(like, start, count)
            .Select(Path.GetFileNameWithoutExtension)!);
    }
}