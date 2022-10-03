using Useful_training.Core.Neural_network;

namespace NeuralNetworkApi.ViewModel
{
    public class neuralNetworkTrainerWorker
    {
        public string NeuralNetworkName { get; set; }
        public string DataSetListName { get; set; }
        public NeuralNetworkTrainer neuralNetworkTrainer { get; set; }
    }
}
