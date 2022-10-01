using FileManager.Exceptions;
using Newtonsoft.Json;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;

namespace FileManager
{
	public class NeuralNetworkWarehouse : Warehouse, INeuralNetworkWarehouse
	{
		public NeuralNetworkWarehouse() : base("SavedNeuralNetwork\\", typeof(INeural_Network))
		{
		}
    }
}