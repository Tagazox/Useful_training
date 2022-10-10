using Useful_training.Core.NeuralNetwork.ValueObject;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Useful_training.Applicative.Application.UseCases.DataSetsLists.Get.ViewModels;

public class DataSetsListViewModel
{
    public DataSetsListViewModel(List<DataSet> dataSets,string name)
    {
        Name = name;
        DataSets = dataSets;
    }
    
    public string Name { get; }
    public int Count => DataSets.Count;
    public int InputsLength => DataSets.FirstOrDefault()?.Inputs.Count ?? 0;
    public int TargetedOutputsLength => DataSets.FirstOrDefault()?.TargetOutput.Count ?? 0;
    public List<DataSet> DataSets { get; }
}