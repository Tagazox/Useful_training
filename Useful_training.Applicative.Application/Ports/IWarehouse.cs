namespace Useful_training.Applicative.Application.Ports;

public interface IWarehouse
{
	public IEnumerable<string> SearchAvailable(string? seamsLike, int start, int count);
	public T Retrieve<T>(string name); 
	public Task Save<T>(T dataToSave,string name);
	public Task Override<T>(T dataToOverride, string name);
}