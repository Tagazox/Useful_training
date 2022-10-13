using Useful_training.Applicative.Application.Ports;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Get;

public class GetDataSetsListByNameUseCase:IGetDataSetsListByNameUseCase
{
    private readonly IDataSetsListWarehouse _dataSetsListWarehouse;

    public GetDataSetsListByNameUseCase(IDataSetsListWarehouse dataSetsListWarehouse)
    {
        _dataSetsListWarehouse = dataSetsListWarehouse;
    }

    public DataSetsListViewModel Execute(string name)
    {
        return new DataSetsListViewModel(_dataSetsListWarehouse.Retrieve<List<DataSet>>(name),name);
    }
}
