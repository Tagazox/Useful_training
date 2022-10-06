using Microsoft.AspNetCore.SignalR;
using Useful_training.Core.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.ValueObject;
using Useful_training.Applicative.NeuralNetworkApi.Adapter;
using Useful_training.Core.NeuralNetwork.NeuralNetwork;
using Useful_training.Core.NeuralNetwork.Observer.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers;
using Useful_training.Core.NeuralNetwork.Warehouse.Interfaces;

namespace Useful_training.Applicative.NeuralNetworkApi.Hubs
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
            containerAdapter.NeuralNetwork = NeuralNetworkWarehouse.Retrieve<NeuralNetwork>(NeuralNetworkName);
            containerAdapter.DataSets = DatasetListWarehouse.Retrieve<List<DataSet>>(DataSetListName);
            NeuralNetworkTrainer neuralNetworkTrainer = new NeuralNetworkTrainer(containerAdapter);
            neuralNetworkTrainer.AttachObserver(this);
            try
            {
                neuralNetworkTrainer.TrainNeuralNetwork();
            }
            catch (Exception e)
            {
                Clients.Caller.SendAsync("OnTrainError", e);
                throw e;
            }
            NeuralNetworkWarehouse.Override(containerAdapter.NeuralNetwork, NeuralNetworkName);
        }
        public void Update(INeuralNetworkObservableData subject)
        {
            Clients.Caller.SendAsync("TrainIterateOnce", subject);
        }
    }
}
