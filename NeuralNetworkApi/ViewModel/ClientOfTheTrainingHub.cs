using Useful_training.Core.NeuralNetwork;

namespace Useful_training.Applicative.NeuralNetworkApi.ViewModel
{
    public class neuralNetworkTrainerWorker
    {
        public string NeuralNetworkName { get; set; }
        public string DataSetListName { get; set; }
        public NeuralNetworkTrainer neuralNetworkTrainer { get; set; }
    }
}
