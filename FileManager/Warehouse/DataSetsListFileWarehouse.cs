using Useful_training.Core.NeuralNetwork.ValueObject;
using Useful_training.Infrastructure.FileManager.Base;
using Useful_training.Infrastructure.FileManager.Warehouse.Interfaces;

namespace Useful_training.Infrastructure.FileManager.Warehouse;

public sealed class DataSetsListFileWarehouse : FileWarehouse, IDataSetsListWarehouse
{
	public DataSetsListFileWarehouse(string rootFolderPath) : base(rootFolderPath, typeof(IList<DataSet>))
	{
	}
}