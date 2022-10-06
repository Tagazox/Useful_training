using Newtonsoft.Json;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;
using Useful_training.Infrastructure.FileManager.Exception;

namespace Useful_training.Infrastructure.FileManager.Base
{
    public abstract class FileWarehouse : IWarehouse
    {
        private readonly string RootFolder;
        private readonly Type TypeToSave;
        private readonly string FileExtention = ".json";

        public FileWarehouse(string rootFolder, Type typeToSave)
        {
            RootFolder = rootFolder;
            TypeToSave = typeToSave;
            if (!Directory.Exists(RootFolder))
                Directory.CreateDirectory(RootFolder);
        }

        public T Retrieve<T>(string name)
        {
            string FilePath = RetrieveFilePath(name);
            if (!typeof(T).IsAssignableTo(TypeToSave))
                throw new InvalidCastException(
                    $"Type of T need to be assignable to a type of {TypeToSave.FullName} you give a type of {typeof(T).FullName}");
            if (typeof(T).IsAbstract || typeof(T).IsInterface)
                throw new InvalidCastException($"Type of T can't be a interface or an abstract class");

            if (!File.Exists(FilePath))
                throw new CantFindException($"Any {TypeToSave.Name} with this name has been found");

            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(FilePath));
            }
            catch (System.Exception e)
            {
                throw new JsonException($"Can't parse the JSON, file corrupted; {e.Message}");
            }
        }

        public async Task Save<T>(T ObjectToSave, string name)
        {
            if (!typeof(T).IsAssignableTo(TypeToSave))
                throw new System.Exception(
                    $"Type of T need to be assignable to a type of {TypeToSave.FullName} you give a type of {typeof(T).Name}");

            string json = JsonConvert.SerializeObject(ObjectToSave, Formatting.Indented);
            string FilePath = RetrieveFilePath(name);

            if (File.Exists(FilePath))
                throw new AlreadyExistException($"A {TypeToSave.Name} with this name already exist");

            await File.WriteAllTextAsync(FilePath, json);
        }

        public async Task Override<T>(T ObjectToSave, string name)
        {
            if (!typeof(T).IsAssignableTo(TypeToSave))
                throw new System.Exception(
                    $"Type of T need to be assignable to a type of {TypeToSave.Name} you give a type of {typeof(T).Name}");

            string json = JsonConvert.SerializeObject(ObjectToSave, Formatting.Indented);
            string FilePath = RetrieveFilePath(name);

            if (!File.Exists(FilePath))
                throw new CantFindException(
                    $"Any {TypeToSave.Name} with this name has been found, you can override only existing item");

            await File.WriteAllTextAsync(FilePath, json);
        }

        public IEnumerable<string> SearchAvailable(string? seamsLike, int start, int count)
        {
            string[] fileList = Directory.EnumerateFiles(RootFolder).ToArray();
            return seamsLike != null
                ? fileList.Where(s => s.Contains(seamsLike)).Skip(start).Take(count)
                : fileList.Skip(start).Take(count);
        }

        private string RetrieveFilePath(string name)
        {
            return Path.Combine(RootFolder, name + FileExtention);
        }
    }
}