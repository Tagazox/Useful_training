namespace Useful_training.Core.NeuralNetwork.Interfaces
{
	public interface INeuralNetworkObservable
	{
        public void AttachObserver(INeuralNetworkTrainerObserver observer);
        public void DetachObserver(INeuralNetworkTrainerObserver observer);
        public void NotifyObserver(INeuralNetworkObservableData datas);
    }
}
