using Useful_training.Applicative.Application.Ports;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Get;

public class SearchDataSetsListByNameUseCase : ISearchDataSetsListByNameUseCase
{
    private readonly IDataSetsListWarehouse _dataSetsListWarehouse;

    public SearchDataSetsListByNameUseCase(IDataSetsListWarehouse dataSetsListWarehouse)
    {
        _dataSetsListWarehouse = dataSetsListWarehouse;
    }

    public DataSetsAvailableViewModel Execute(string? like,int start=0,int count=10)
    {
        return new DataSetsAvailableViewModel(_dataSetsListWarehouse.SearchAvailable(like ?? "", start, count)
            .Select(Path.GetFileNameWithoutExtension)!);
    }
}