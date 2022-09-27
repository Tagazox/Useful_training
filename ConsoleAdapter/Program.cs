// See https://aka.ms/new-console-template for more information
using ConsoleAdapter;
using Useful_training.Core.Neural_network;

Console.WriteLine("Hello, World!");

ContainerConsoleAdapter containerConsoleAdapter = new ContainerConsoleAdapter();

INeuralNetworkTrainer trainer = new NeuralNetworkTrainer(containerConsoleAdapter);

ConsoleTrainerAdapter trainerAdapter = new ConsoleTrainerAdapter(trainer);
trainerAdapter.FollowNetworkTraining();