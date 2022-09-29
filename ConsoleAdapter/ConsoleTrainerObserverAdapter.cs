using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;

namespace ConsoleAdapter
{
    internal class ConsoleTrainerObserverAdapter: INeuralNetworkTrainerObserver
    {
        NeuralNetworkTrainer trainer;

        public ConsoleTrainerObserverAdapter(NeuralNetworkTrainer InjectedTrainer)
        {
            trainer = InjectedTrainer;
            trainer.Attach(this);
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
            Console.Write(string.Concat(subject.DataSet.Values.Select(v => $"{v} ")));
            Console.SetCursorPosition(30, 3);
            Console.Write(string.Concat(subject.DataSet.Targets.Select(v => $"{v} ")));
            Console.SetCursorPosition(30, 4);
            Console.Write(string.Concat(subject.Results.Select(v => $"{v} ")));
            Console.SetCursorPosition(30, 5);
            Console.Write(string.Concat(subject.DeltasErrors.Select(v => $"{v} ")));


        }
    }
}
