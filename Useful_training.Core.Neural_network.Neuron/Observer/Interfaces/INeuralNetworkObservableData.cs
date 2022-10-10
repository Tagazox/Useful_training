using Useful_training.Core.NeuralNetwork.ValueObject;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace Useful_training.Core.NeuralNetwork.Observer.Interfaces;

public interface INeuralNetworkObservableData
{
	public DataSet DataSet { get; }
	public IList<double> Results { get; }
	public IEnumerable<double> DeltasErrors { get; }
}