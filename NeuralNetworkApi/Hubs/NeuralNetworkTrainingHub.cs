using Microsoft.AspNetCore.SignalR;
using NeuralNetworkApi.Adapter;
using NeuralNetworkApi.ViewModel;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.ValueObject;

namespace NeuralNetworkApi.Hubs
{
    public class NeuralNetworkTrainingHub : Hub, INeuralNetworkTrainerObserver
    {
        private readonly INeuralNetworkWarehouse NeuralNetworkWarehouse;
        private readonly IDataSetsListWarehouse DatasetListWarehouse;
        public NeuralNetworkTrainingHub(INeuralNetworkWarehouse neuralNetworkWarehouse, IDataSetsListWarehouse datasetListWarehouse)
        {
            NeuralNetworkWarehouse = neuralNetworkWarehouse;
            DatasetListWarehouse = datasetListWarehouse;
        }
        public void TrainNeuralNetwork(string NeuralNetworkName, string DataSetListName)
        {
            CreateWorkerAndAttacheTheClient(NeuralNetworkName, DataSetListName);
            Clients.Caller.SendAsync("Train_finished", NeuralNetworkName);
        }

        private void CreateWorkerAndAttacheTheClient(string NeuralNetworkName, string DataSetListName)
        {
            NeuralNetworkTrainerContainerAdapter containerAdapter = new NeuralNetworkTrainerContainerAdapter();
            containerAdapter.Neural_Network = NeuralNetworkWarehouse.Retreive<NeuralNetwork>(NeuralNetworkName);
            containerAdapter.DataSets = DatasetListWarehouse.Retreive<List<DataSet>>(DataSetListName);
            NeuralNetworkTrainer neuralNetworkTrainer = new NeuralNetworkTrainer(containerAdapter);
            neuralNetworkTrainer.Attach(this);
            try
            {
                neuralNetworkTrainer.TrainNeuralNetwork();
            }
            catch (Exception e)
            {
                Clients.Caller.SendAsync("OnTrainError", e);
                throw e;
            }
            NeuralNetworkWarehouse.Override(containerAdapter.Neural_Network, NeuralNetworkName);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

        }

        public void Update(INeuralNetworkObservableData subject)
        {
            Clients.Caller.SendAsync("TrainIterateOnce", subject);
        }
    }
}
