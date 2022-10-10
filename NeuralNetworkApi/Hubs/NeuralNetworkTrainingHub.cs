using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Useful_training.Applicative.Application.UseCases.Training.Interfaces;

namespace Useful_training.Applicative.NeuralNetworkApi.Hubs;

public class NeuralNetworkTrainingHub : Hub
{
    private readonly ITrainNeuralNetworkUseCase _trainNeuralNetworkUseCase;

    public NeuralNetworkTrainingHub([FromServices] ITrainNeuralNetworkUseCase trainNeuralNetworkUseCase)
    {
        _trainNeuralNetworkUseCase = trainNeuralNetworkUseCase;
    }
    public void TrainNeuralNetwork(string neuralNetworkName, string dataSetListName)
    {
        _trainNeuralNetworkUseCase.Execute(neuralNetworkName,dataSetListName,Clients.Caller);
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _trainNeuralNetworkUseCase.Cancel();
        await base.OnDisconnectedAsync(exception);
    }
}