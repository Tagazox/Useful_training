using FileManager.Exceptions;
using Newtonsoft.Json;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;

namespace FileManager
{
	public class NeuralNetworkFileWarehouse : FileWarehouse, INeuralNetworkWarehouse
	{
		public NeuralNetworkFileWarehouse(string rootFolderPath) : base(rootFolderPath, typeof(INeuralNetwork))
		{
		}
    }
}