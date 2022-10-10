using Useful_training.Core.NeuralNetwork.Observer.Interfaces;
using Useful_training.Core.NeuralNetwork.Trainers;

namespace Useful_training.Applicative.ConsoleAdapter
{
	internal class ConsoleTrainerObserverAdapter: INeuralNetworkTrainerObserver
    {
        NeuralNetworkTrainer trainer;

        public ConsoleTrainerObserverAdapter(NeuralNetworkTrainer InjectedTrainer)
        {
            trainer = InjectedTrainer;
            trainer.AttachObserver(this);
        }
        public void FollowNetworkTraining()
        {
            Console.Clear();
			Console.WriteLine("-----------------------------------");
			Console.WriteLine("Observation de l'entrainement du réseau de neurone");
            Console.WriteLine("Entrée(s) du dataset testé :");
            Console.WriteLine("Sortie(s) attendu :");
            Console.WriteLine("Sortie(s) du réseau :");
            Console.WriteLine("Delta d'erreur :");

            trainer.TrainNeuralNetwork();
        }

		public void Update(INeuralNetworkObservableData subject)
		{
			Console.SetCursorPosition(30, 2);
            Console.Write(string.Concat(subject.DataSet.Inputs.Select(v => $"{v} ")));
            Console.SetCursorPosition(30, 3);
            Console.Write(string.Concat(subject.DataSet.TargetOutput.Select(v => $"{v} ")));
            Console.SetCursorPosition(30, 4);
            Console.Write(string.Concat(subject.Results.Select(v => $"{v} ")));
            Console.SetCursorPosition(30, 5);
            Console.Write(string.Concat(subject.DeltasErrors.Select(v => $"{v} ")));


        }
    }
}
