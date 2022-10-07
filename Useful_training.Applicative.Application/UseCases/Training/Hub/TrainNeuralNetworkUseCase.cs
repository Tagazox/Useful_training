using Microsoft.AspNetCore.SignalR;
using Useful_training.Applicative.Application.Adapter;
using Useful_training.Applicative.Application.UseCases.Training.Interfaces;
using Useful_training.Core.NeuralNetwork.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.Observer.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers;
using Useful_training.Core.NeuralNetwork.Trainers.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;

namespace Useful_training.Applicative.Application.UseCases.Training.Hub;

public class TrainNeuralNetworkUseCase : ITrainNeuralNetworkUseCase
{
    private readonly INeuralNetworkWarehouse NeuralNetworkWarehouse;
    private readonly IDataSetsListWarehouse DatasetListWarehouse;

    private IClientProxy? HttpClient;
    
    public TrainNeuralNetworkUseCase(INeuralNetworkWarehouse neuralNetworkWarehouse, IDataSetsListWarehouse datasetListWarehouse)
    {
        NeuralNetworkWarehouse = neuralNetworkWarehouse;
        DatasetListWarehouse = datasetListWarehouse;
    }

    public void Execute(string neuralNetworkName, string dataSetsListName, IClientProxy httpClient)
    {
        HttpClient = httpClient;
        NeuralNetworkTrainerContainerAdapter containerAdapter = new NeuralNetworkTrainerContainerAdapter();

        containerAdapter.NeuralNetwork = NeuralNetworkWarehouse.Retrieve<NeuralNetwork>(neuralNetworkName);
        containerAdapter.DataSets =DatasetListWarehouse.Retrieve<List<DataSet>>(dataSetsListName);
        
        NeuralNetworkTrainer neuralNetworkTrainer = new NeuralNetworkTrainer(containerAdapter);
        neuralNetworkTrainer.AttachObserver(this);
        
        try
        {
            neuralNetworkTrainer.TrainNeuralNetwork();
        }
        catch (Exception e)
        {
            HttpClient.SendAsync("OnTrainError", e);
            throw;
        } 
        
        NeuralNetworkWarehouse.Override(containerAdapter.NeuralNetwork, neuralNetworkName);
    }

    public void Update(INeuralNetworkObservableData subject)
    {
        HttpClient?.SendAsync("TrainIterateOnce", subject);
    }

}