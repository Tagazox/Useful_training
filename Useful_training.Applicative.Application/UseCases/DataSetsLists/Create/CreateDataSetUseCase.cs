using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.ViewModels;
using Useful_training.Core.NeuralNetwork.ValueObject;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Create;

public class CreateDataSetsListUseCase
{
    private readonly IDataSetsListWarehouse DataSetsListWarehouse;

    public CreateDataSetsListUseCase(IDataSetsListWarehouse dataSetsListWarehouse)
    {
        DataSetsListWarehouse = dataSetsListWarehouse;
    }

    public async Task<DataSetListCreatedViewModel> ExecuteAsync(string name, List<DataSet> dataSets)
    {
        if (!dataSets.Any())
            throw new ArgumentException("Data set list can't be null");
        if (!dataSets.TrueForAll(d => d.Inputs.Count == dataSets.First().Inputs.Count))
            throw new ArgumentException("Input need to always have the same count of data");
        if (!dataSets.TrueForAll(x => x.TargetOutput.Count == dataSets.First().TargetOutput.Count))
            throw new ArgumentException("Outputs need to always have the same count of data");
        if(dataSets.Select(d => d.Inputs).Distinct().Count() != dataSets.Count)
            throw new ArgumentException("You cannot have duplicate input");
        await DataSetsListWarehouse.Save(dataSets, name);
        return new DataSetListCreatedViewModel(name);
    }
}