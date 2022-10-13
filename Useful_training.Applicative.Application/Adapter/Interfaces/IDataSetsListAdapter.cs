using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.Application.Adapter.Interfaces;

public interface IDataSetsListAdapter
{
    public List<double> Inputs { get; set; }
    public List<double> TargetOutputs { get; set; }

    public DataSet GetDataSet();
}