using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network.Interface
{
	public interface IWarehouse
	{
		public IEnumerable<string> SearchAvailable(string seamsLike, int start, int count);
		public T Retreive<T>(string name); 
		public Task Save<T>(T neuralNetToSave,string name);
		public Task Override<T>(T neuralNetToSave, string name);
	}
}
