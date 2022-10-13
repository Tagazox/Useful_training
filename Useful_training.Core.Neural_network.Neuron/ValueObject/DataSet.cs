using System.Text.Json.Serialization;

namespace Useful_training.Core.NeuralNetwork.ValueObject;

[Serializable]
public class DataSet : ValueObject
{
    public List<double> Inputs { get; private set; }
    public List<double> TargetOutput { get; private set; }

    [JsonConstructor]
    public DataSet()
    {
        Inputs = new List<double>();
        TargetOutput = new List<double>();
    }

    public DataSet(List<double> values, List<double> targets)
    {
        Inputs = values;
        TargetOutput = targets;
    }
}