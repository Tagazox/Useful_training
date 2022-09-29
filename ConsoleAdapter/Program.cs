// See https://aka.ms/new-console-template for more information
using ConsoleAdapter;
using Useful_training.Core.Neural_network;

Console.WriteLine("Hello, World!");

ContainerConsoleAdapter containerConsoleAdapter = new ContainerConsoleAdapter();

NeuralNetworkTrainer trainer = new NeuralNetworkTrainer(containerConsoleAdapter);

ConsoleTrainerObserverAdapter trainerAdapter = new ConsoleTrainerObserverAdapter(trainer);
trainerAdapter.FollowNetworkTraining();