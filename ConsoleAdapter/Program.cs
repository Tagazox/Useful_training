using Useful_training.Applicative.ConsoleAdapter;
using Useful_training.Core.NeuralNetwork.Trainers;

ContainerConsoleAdapter containerConsoleAdapter = new ContainerConsoleAdapter();

NeuralNetworkTrainer trainer = new NeuralNetworkTrainer(containerConsoleAdapter);

ConsoleTrainerObserverAdapter trainerAdapter = new ConsoleTrainerObserverAdapter(trainer);
trainerAdapter.FollowNetworkTraining();