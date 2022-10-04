using FileManager.Exceptions;
using Newtonsoft.Json;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.ValueObject;

namespace FileManager
{
	public class DataSetsListFileWarehouse : FileWarehouse, IDataSetsListWarehouse
	{
		public DataSetsListFileWarehouse(string rootFolderPath) : base(rootFolderPath, typeof(List<DataSet>))
		{
		}
	}
}