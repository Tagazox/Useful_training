using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;

public interface INeuralNetwork : ISerializable
{
    public bool IsUnstable { get; }
    public IList<double> LastCalculationResults { get; }
    public void BackPropagate(List<double> targets);
    public IList<double> Calculate(IList<double> inputs);
    public void Reset();
}