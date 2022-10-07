using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;
using Useful_training.Core.NeuralNetwork.ValueObject;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Get;

public class GetDataSetsListByNameUseCase:IGetDataSetsListByNameUseCase
{
    private readonly IDataSetsListWarehouse DataSetsListWarehouse;

    public GetDataSetsListByNameUseCase(IDataSetsListWarehouse dataSetsListWarehouse)
    {
        DataSetsListWarehouse = dataSetsListWarehouse;
    }

    public DataSetsListViewModel Execute(string name)
    {
        return new DataSetsListViewModel(DataSetsListWarehouse.Retrieve<List<DataSet>>(name),name);
    }
}
