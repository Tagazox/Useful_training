using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;
using Useful_training.Infrastructure.FileManager.Base;

namespace Useful_training.Infrastructure.FileManager.Warehouse
{
	public sealed class NeuralNetworkFileWarehouse : FileWarehouse, INeuralNetworkWarehouse
	{
		public NeuralNetworkFileWarehouse(string rootFolderPath) : base(rootFolderPath, typeof(INeuralNetwork))
		{
		}
    }
}