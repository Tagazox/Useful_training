using Useful_training.Core.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Infrastructure.FileManager
{
	public sealed class DataSetsListFileWarehouse : FileWarehouse, IDataSetsListWarehouse
	{
		public DataSetsListFileWarehouse(string rootFolderPath) : base(rootFolderPath, typeof(IList<DataSet>))
		{
		}
	}
}