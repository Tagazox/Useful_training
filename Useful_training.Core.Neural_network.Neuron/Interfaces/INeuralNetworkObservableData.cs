using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.Interfaces
{
	public interface INeuralNetworkObservableData
	{
		public DataSet DataSet { get; }
		public IList<double> Results { get; }
		public double[] DeltasErrors { get; }
	}
}
