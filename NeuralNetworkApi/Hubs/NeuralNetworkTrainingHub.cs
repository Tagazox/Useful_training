using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Useful_training.Applicative.Application.UseCases.Training.Interfaces;

namespace Useful_training.Applicative.NeuralNetworkApi.Hubs
{
    public class NeuralNetworkTrainingHub : Hub
    {
        private readonly ITrainNeuralNetworkUseCase TrainNeuralNetworkUseCase;

        public NeuralNetworkTrainingHub([FromServices] ITrainNeuralNetworkUseCase trainNeuralNetworkUseCase)
        {
            TrainNeuralNetworkUseCase = trainNeuralNetworkUseCase;
        }
        public void TrainNeuralNetwork(string neuralNetworkName, string dataSetListName)
        {
            TrainNeuralNetworkUseCase.Execute(neuralNetworkName,dataSetListName,Clients.Caller);
        }
    }
}
