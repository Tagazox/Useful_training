namespace Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;

public interface IWarehouse
{
	public IEnumerable<string> SearchAvailable(string? seamsLike, int start, int count);
	public T Retrieve<T>(string name); 
	public Task Save<T>(T neuralNetToSave,string name);
	public Task Override<T>(T neuralNetToSave, string name);
}