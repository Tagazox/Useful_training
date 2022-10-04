namespace Useful_training.Core.NeuralNetwork.Interfaces
{
	public interface IDataSetsListWarehouse
	{
		public IEnumerable<string> SearchAvailable(string seamsLike, int start, int count);
		public T Retreive<T>(string name);
		public Task Save<T>(T DataSetListToSave,string name);
		public Task Override<T>(T DataSetListToSave,string name);
	}
}
