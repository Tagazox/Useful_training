using Microsoft.AspNetCore.SignalR;
using Useful_training.Applicative.Application.Adapter;
using Useful_training.Applicative.Application.Ports;
using Useful_training.Applicative.Application.UseCases.Training.Interfaces;
using Useful_training.Core.NeuralNetwork.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.Observer.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers;
using Useful_training.Core.NeuralNetwork.Trainers.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.Application.UseCases.Training.Hub;

public class TrainNeuralNetworkUseCase : ITrainNeuralNetworkUseCase
{
    private readonly INeuralNetworkWarehouse _neuralNetworkWarehouse;
    private readonly IDataSetsListWarehouse _datasetListWarehouse;
    private INeuralNetworkTrainer? _neuralNetworkTrainer;
    private IClientProxy? _httpClient;

    public TrainNeuralNetworkUseCase(INeuralNetworkWarehouse neuralNetworkWarehouse,
        IDataSetsListWarehouse datasetListWarehouse)
    {
        _neuralNetworkWarehouse = neuralNetworkWarehouse;
        _datasetListWarehouse = datasetListWarehouse;
    }

    public void Execute(string neuralNetworkName, string dataSetsListName, IClientProxy httpClient)
    {
        _httpClient = httpClient;
        NeuralNetworkTrainerContainerAdapter containerAdapter = new NeuralNetworkTrainerContainerAdapter
        (
            _datasetListWarehouse.Retrieve<List<DataSet>>(dataSetsListName),
            _neuralNetworkWarehouse.Retrieve<NeuralNetwork>(neuralNetworkName)
        );

        _neuralNetworkTrainer = new NeuralNetworkTrainer(containerAdapter);
        _neuralNetworkTrainer.AttachObserver(this);

        try
        {
            _neuralNetworkTrainer.TrainNeuralNetwork();
        }
        catch (Exception e)
        {
            _httpClient.SendAsync("OnTrainError", e);
            throw;
        }

        _neuralNetworkTrainer.DetachObserver(this);
        _neuralNetworkWarehouse.Override(containerAdapter.NeuralNetwork, neuralNetworkName);
        _httpClient?.SendAsync("Train_finished");
    }

    public void Cancel()
    {
        _neuralNetworkTrainer?.Destroy();
    }

    public void Update(INeuralNetworkObservableData subject)
    {
        _httpClient?.SendAsync("TrainIterateOnce", subject);
    }
}