using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;

namespace FileManager.Interfaces
{
	public interface INeuralNetworkWarehouse
	{
		public IEnumerable<string> RetreiveNeuralNetworkAvailable(string seamsLike, int start, int count);
		public INeural_Network RetreiveNeuralNetwork(string name);
		public void SaveNeuralNetwork(INeural_Network neuralNetToSave,string name);
	}
}
