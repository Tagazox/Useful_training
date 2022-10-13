using Useful_training.Applicative.Application.Adapter;
using Useful_training.Applicative.Application.Adapter.Interfaces;
using Useful_training.Applicative.Application.Ports;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.Interfaces;
using Useful_training.Applicative.Application.UseCases.DataSetsLists.Create.ViewModels;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Create;

public class CreateDataSetsListUseCase : ICreateDataSetsListUseCase
{
    private readonly IDataSetsListWarehouse _dataSetsListWarehouse;

    public CreateDataSetsListUseCase(IDataSetsListWarehouse dataSetsListWarehouse)
    {
        _dataSetsListWarehouse = dataSetsListWarehouse;
    }

    public async Task<DataSetListCreatedViewModel> ExecuteAsync(string name, List<DataSetsListAdapter> dataSetsAdapters)
    {
        if (!dataSetsAdapters.Any())
            throw new ArgumentException("Data set list can't be null");
        if (dataSetsAdapters.Any(d => d.Inputs.Count == 0) || dataSetsAdapters.Any(d => d.TargetOutputs.Count == 0) )
            throw new ArgumentException("Inputs and TargetOutputs need to have at least one value");
        if (!dataSetsAdapters.TrueForAll(d => d.Inputs.Count == dataSetsAdapters.First().Inputs.Count))
            throw new ArgumentException("Inputs need to always have the same count of data");
        if (!dataSetsAdapters.TrueForAll(x => x.TargetOutputs.Count == dataSetsAdapters.First().TargetOutputs.Count))
            throw new ArgumentException("Outputs need to always have the same count of data");
        if(dataSetsAdapters.Select(d => d.Inputs).Distinct().Count() != dataSetsAdapters.Count)
            throw new ArgumentException("You cannot have duplicate input");
        
        await _dataSetsListWarehouse.Save(dataSetsAdapters.Select(d => d.GetDataSet()).ToList(), name);
        return new DataSetListCreatedViewModel(name);
    }

}