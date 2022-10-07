using Microsoft.AspNetCore.SignalR;
using Useful_training.Core.NeuralNetwork.Observer.Interfaces;

namespace Useful_training.Applicative.Application.UseCases.Training.Interfaces;

public interface ITrainNeuralNetworkUseCase : INeuralNetworkTrainerObserver
{
    void Execute(string neuralNetworkName, string dataSetsListName, IClientProxy httpClient);
}