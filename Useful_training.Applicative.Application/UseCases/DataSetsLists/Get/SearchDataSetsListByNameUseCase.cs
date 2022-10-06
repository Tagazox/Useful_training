using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Get;

public class SearchDataSetsListByNameUseCase : ISearchDataSetsListByNameUseCase
{
    private readonly IDataSetsListWarehouse DataSetsListWarehouse;

    public SearchDataSetsListByNameUseCase(IDataSetsListWarehouse dataSetsListWarehouse)
    {
        DataSetsListWarehouse = dataSetsListWarehouse;
    }

    public DataSetsAvailableViewModel Execute(string? like,int start=0,int count=10)
    {
        return new DataSetsAvailableViewModel(DataSetsListWarehouse.SearchAvailable(like ?? "", start, count)
            .Select(p => Path.GetFileNameWithoutExtension(p) ?? ""));
    }
}