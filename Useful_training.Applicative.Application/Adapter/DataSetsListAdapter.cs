using Newtonsoft.Json;
using Useful_training.Applicative.Application.Adapter.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.Application.Adapter;

[Serializable]
public class DataSetsListAdapter : IDataSetsListAdapter
{
    [JsonConstructor]
    public DataSetsListAdapter()
    {
        Inputs = new List<double>();
        TargetOutputs = new List<double>();
    }
    public DataSetsListAdapter(List<double> inputs, List<double> targetOutputs)
    {
        Inputs = inputs;
        TargetOutputs = targetOutputs;
    }
    public List<double> Inputs { get; set; }
    public List<double> TargetOutputs { get; set; }
    public DataSet GetDataSet()
    {
        return new DataSet(Inputs, TargetOutputs);
    }
}