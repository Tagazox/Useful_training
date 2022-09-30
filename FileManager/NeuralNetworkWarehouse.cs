using FileManager.Exceptions;
using FileManager.Interfaces;
using Newtonsoft.Json;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;

namespace FileManager
{
	public class NeuralNetworkWarehouse : INeuralNetworkWarehouse
	{
		private string RootFolder = "SavedNeuralNetwork\\";
		private string FileExtention = ".json";
		public NeuralNetworkWarehouse()
		{
			if(!Directory.Exists(RootFolder))
				Directory.CreateDirectory(RootFolder);
		}
		public INeural_Network RetreiveNeuralNetwork(string name)
		{
			string FilePath = RetreiveFilePath(name);
			if (!File.Exists(FilePath))
				throw new FileNotFoundException("Any neural network with this name has been found");
			try
			{
				return JsonConvert.DeserializeObject<INeural_Network>(File.ReadAllText(FilePath));
			}
			catch (Exception)
			{

				throw new JsonException("Can't parse the JSON file corrupted neural network");
			}
		}

		public IEnumerable<string> RetreiveNeuralNetworkAvailable(string seamsLike, int start, int count)
		{
			return Directory.EnumerateFiles(RootFolder).Where(s => seamsLike.Contains(s)).Skip(0).Take(count);
		}

		public void SaveNeuralNetwork(INeural_Network neuralNetToSave, string name)
		{
			string json = JsonConvert.SerializeObject(neuralNetToSave, Formatting.Indented);
			string FilePath = RetreiveFilePath(name);
			if (File.Exists(FilePath))
				throw new FileAlreadyExistException("A neural network with this name already exist");
			File.WriteAllText(FilePath, json);
		}

		private string RetreiveFilePath(string name)
		{
			return Path.Combine(RootFolder, name + FileExtention);
		}
	}
}