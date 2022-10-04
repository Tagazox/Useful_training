using FileManager.Exceptions;
using Newtonsoft.Json;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;

namespace FileManager
{
    public abstract class FileWarehouse : IWarehouse
    {
        private string RootFolder;
        private Type TypeToSave;
        private string FileExtention = ".json";
        public FileWarehouse(string rootFolder, Type typeToSave)
        {
            RootFolder = rootFolder;
            TypeToSave = typeToSave;
            if (!Directory.Exists(RootFolder))
                Directory.CreateDirectory(RootFolder);
        }
        public T Retreive<T>(string name)
        {
            string FilePath = RetreiveFilePath(name);
            if (!typeof(T).IsAssignableTo(TypeToSave))
                throw new Exception($"Type of T need to be assignable to a type of {TypeToSave.FullName} you give a type of {typeof(T).FullName}");
            if (typeof(T).IsAbstract || typeof(T).IsInterface)
                throw new Exception($"Type of T can't be a interface or an abstract class");
            
            if (!File.Exists(FilePath))
                throw new CantFindException($"Any {TypeToSave.Name} with this name has been found");

            try
            {
                return (JsonConvert.DeserializeObject<T>(File.ReadAllText(FilePath)));
            }
            catch (Exception e)
            {

                throw new JsonException($"Can't parse the JSON, file corrupted; {e.Message}");
            }
        }

        public async Task Save<T>(T ObjectToSave, string name)
        {
            if (!typeof(T).IsAssignableTo(TypeToSave))
                throw new Exception($"Type of T need to be assignable to a type of {TypeToSave.FullName} you give a type of {typeof(T).Name}");

            string json = JsonConvert.SerializeObject(ObjectToSave, Formatting.Indented);
            string FilePath = RetreiveFilePath(name);

            if (File.Exists(FilePath))
                throw new AlreadyExistException($"A {TypeToSave.Name} with this name already exist");

            await File.WriteAllTextAsync(FilePath, json);
        }

        public  async Task Override<T>(T ObjectToSave, string name)
        {
            if (!typeof(T).IsAssignableTo(TypeToSave))
                throw new Exception($"Type of T need to be assignable to a type of {TypeToSave.Name} you give a type of {typeof(T).Name}");

            string json = JsonConvert.SerializeObject(ObjectToSave, Formatting.Indented);
            string FilePath = RetreiveFilePath(name);

            await File.WriteAllTextAsync(FilePath, json);
        }

        public IEnumerable<string> SearchAvailable(string seamsLike, int start, int count)
        {
            string[] fileList = Directory.EnumerateFiles(RootFolder).ToArray();
            return fileList.Where(s => s.Contains(seamsLike)).Skip(start).Take(count);
        }

        private string RetreiveFilePath(string name)
        {
            return Path.Combine(RootFolder, name + FileExtention);
        }


    }
}