namespace Useful_training.Core.NeuralNetwork.Trainers.Interfaces
{
    public interface INeuralNetworkTrainer : INeuralNetworkObservable
    {
        void TrainNeuralNetwork();
    }
}