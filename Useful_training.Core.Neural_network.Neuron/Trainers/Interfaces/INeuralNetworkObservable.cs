using Useful_training.Core.NeuralNetwork.Observer.Interfaces;

namespace Useful_training.Core.NeuralNetwork.Trainers.Interfaces;

public interface INeuralNetworkObservable
{
	public void AttachObserver(INeuralNetworkTrainerObserver observer);
	public void DetachObserver(INeuralNetworkTrainerObserver observer);
}