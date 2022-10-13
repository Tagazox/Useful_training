using Useful_training.Applicative.Application.Ports;
using Useful_training.Core.NeuralNetwork.ValueObject;
using Useful_training.Infrastructure.FileManager.Base;

namespace Useful_training.Infrastructure.FileManager.Warehouse;

public sealed class DataSetsListFileWarehouse : FileWarehouse, IDataSetsListWarehouse
{
	public DataSetsListFileWarehouse(string rootFolderPath) : base(rootFolderPath, typeof(IList<DataSet>))
	{
	}
}