using Useful_training.Core.NeuralNetwork.Interfaces;

namespace Useful_training.Infrastructure.FileManager
{
	public sealed class NeuralNetworkFileWarehouse : FileWarehouse, INeuralNetworkWarehouse
	{
		public NeuralNetworkFileWarehouse(string rootFolderPath) : base(rootFolderPath, typeof(INeuralNetwork))
		{
		}
    }
}