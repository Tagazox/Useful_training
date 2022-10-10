using System.Data;
using Newtonsoft.Json;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;
using Useful_training.Infrastructure.FileManager.Exception;

namespace Useful_training.Infrastructure.FileManager.Base;

public abstract class FileWarehouse : IWarehouse
{
    private readonly string _rootFolder;
    private readonly Type _typeToSave;
    private const string FileExtension = ".json";

    protected FileWarehouse(string rootFolder, Type typeToSave)
    {
        _rootFolder = rootFolder;
        _typeToSave = typeToSave;
        if (!Directory.Exists(_rootFolder))
            Directory.CreateDirectory(_rootFolder);
    }

    public T Retrieve<T>(string name)
    {
        string filePath = RetrieveFilePath(name);
        if (!typeof(T).IsAssignableTo(_typeToSave))
            throw new InvalidCastException(
                $"Type of T need to be assignable to a type of {_typeToSave.FullName} you give a type of {typeof(T).FullName}");
        if (typeof(T).IsAbstract || typeof(T).IsInterface)
            throw new InvalidCastException("Type of T can't be a interface or an abstract class");

        if (!File.Exists(filePath))
            throw new CantFindException($"Any {_typeToSave.Name} with this name has been found");

        try
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath)) ?? throw new NoNullAllowedException();
        }
        catch (System.Exception e)
        {
            throw new JsonException($"Can't parse the JSON, bad file; {e.Message}");
        }
    }

    public async Task Save<T>(T objectToSave, string name)
    {
        if (!typeof(T).IsAssignableTo(_typeToSave))
            throw new System.Exception(
                $"Type of T need to be assignable to a type of {_typeToSave.FullName} you give a type of {typeof(T).Name}");

        string json = JsonConvert.SerializeObject(objectToSave, Formatting.Indented);
        string filePath = RetrieveFilePath(name);

        if (File.Exists(filePath))
            throw new AlreadyExistException($"A {_typeToSave.Name} with this name already exist");

        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task Override<T>(T objectToSave, string name)
    {
        if (!typeof(T).IsAssignableTo(_typeToSave))
            throw new System.Exception(
                $"Type of T need to be assignable to a type of {_typeToSave.Name} you give a type of {typeof(T).Name}");

        string json = JsonConvert.SerializeObject(objectToSave, Formatting.Indented);
        string filePath = RetrieveFilePath(name);

        if (!File.Exists(filePath))
            throw new CantFindException(
                $"Any {_typeToSave.Name} with this name has been found, you can override only existing item");

        await File.WriteAllTextAsync(filePath, json);
    }

    public IEnumerable<string> SearchAvailable(string? seamsLike, int start, int count)
    {
        string[] fileList = Directory.EnumerateFiles(_rootFolder).ToArray();
        return seamsLike != null
            ? fileList.Where(s => s.Contains(seamsLike)).Skip(start).Take(count)
            : fileList.Skip(start).Take(count);
    }

    private string RetrieveFilePath(string name)
    {
        return Path.Combine(_rootFolder, name + FileExtension);
    }
}