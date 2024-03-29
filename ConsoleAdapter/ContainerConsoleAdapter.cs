﻿using System.Globalization;
using Useful_training.Applicative.ConsoleAdapter.Exception;
using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Applicative.ConsoleAdapter
{
	internal class ContainerConsoleAdapter : INeuralNetworkTrainerContainer
	{
		public INeuralNetwork NeuralNetwork { get; private set; }
		public List<DataSet> DataSets { get; private set; }
		private uint _numberOfInput, _numberOutput;
		public ContainerConsoleAdapter()
		{
			
		}
		public void CreateDataSets()
		{
			uint numberOfDataSet;
			List<DataSet> dataSets = new List<DataSet>();

			Console.Clear();
			Console.WriteLine("-----------------------------------");
			Console.WriteLine("Bienvenue dans le créateur de data set !");

			Console.WriteLine("Combien de data set voulez vous créer ?");
			string? sNumberOfDataSet = Console.ReadLine();
			if (!uint.TryParse(sNumberOfDataSet, out numberOfDataSet))
				throw new WrongInputException("Nombre de dataset impossible à parser");

			for (int i = 0; i < numberOfDataSet; i++)
			{
				DataSet ParsedDataSet;
				double[] inputsParsed = null;
				double[] outputsParsed = null;
				bool inputsHasBeenParsed = false;
				while (!inputsHasBeenParsed)
				{
					inputsHasBeenParsed = true;
					Console.WriteLine("-----------------------------------");
					Console.WriteLine($"Data set {i}");
					Console.WriteLine($"Entrée (Attente de {_numberOfInput} entrée(s) séparés par un espace):");
					string? inputs = Console.ReadLine();
					string[] inputsArray = inputs.Split(' ');
					if (inputsArray.Length == _numberOfInput)
					{
						inputsParsed = new double[inputsArray.Length];
						for (int j = 0; j < inputsArray.Length; j++)
						{
							if (!double.TryParse(inputsArray[j], NumberStyles.Any, CultureInfo.InvariantCulture, out inputsParsed[j]))
							{
								inputsHasBeenParsed = false;
								Console.WriteLine("Une entrée est impossible à parser, recommencez");
								break;
							}
						}
					}
					else
					{
						inputsHasBeenParsed = false;
						Console.WriteLine("Nombre d'entrées incohérent recommencez");
					}
				}
				bool outputsHasBeenParsed = false;
				while (!outputsHasBeenParsed)
				{
					outputsHasBeenParsed = true;
					Console.WriteLine("-----------------------------------");
					Console.WriteLine($"Data set {i}");
					Console.WriteLine($"Sorties (Attente de {_numberOutput} sorties(s) séparés par un espace):");
					string? outputs = Console.ReadLine();
					string[] outputsArray = outputs.Split(' ');
					if (outputsArray.Length == _numberOutput)
					{
						outputsParsed = new double[outputsArray.Length];
						for (int j = 0; j < outputsArray.Length; j++)
						{
							if (!double.TryParse(outputsArray[j], NumberStyles.Any, CultureInfo.InvariantCulture, out outputsParsed[j]))
							{
								outputsHasBeenParsed = false;
								Console.WriteLine("Une sortie est impossible à parser, recommencez");
								break;
							}
						}
					}
					else
					{
						outputsHasBeenParsed = false;
						Console.WriteLine("Nombre de sortie incohérent recommencez");
					}
				}
				ParsedDataSet = new DataSet(inputsParsed.ToList(), outputsParsed.ToList());
				dataSets.Add(ParsedDataSet);
			}
			DataSets = dataSets;
		}

		public void CreateNeuralNetwork()
		{
			uint numberOfHiddenLayers, numberOfNeuronesByHiddenLayer;
			double learnRate, momentum;
			NeuronType typeOfNeurons;

			NeuralNetworkDirector _NeuralNetworkDirector = new NeuralNetworkDirector();
			NeuralNetworkBuilder _NeuralNetworkBuilder;
			Console.Clear();
			Console.WriteLine("-----------------------------------");
			Console.WriteLine("Bienvenue dans le créateur de réseau de neurones");

			Console.WriteLine("Combien d'entrées doivent être géré par ce réseau de neurones ?");
			string? sNumberOfInput = Console.ReadLine();
			if (!uint.TryParse(sNumberOfInput, out _numberOfInput))
				throw new WrongInputException("Nombre d'entrées impossible à parser");

			Console.WriteLine("Combien quel est le learn rate ?");
			string? sLearnRate = Console.ReadLine();
			if (!double.TryParse(sLearnRate, NumberStyles.Any, CultureInfo.InvariantCulture, out learnRate))
				throw new WrongInputException("Learn rate impossible à parser");

			Console.WriteLine("Combien quel est le momentum ?");
			string? sMomentum = Console.ReadLine();
			if (!double.TryParse(sMomentum, NumberStyles.Any, CultureInfo.InvariantCulture, out momentum))
				throw new WrongInputException("Momentum impossible à parser");

			

			Console.WriteLine("Combien de sorties doivent être géré par ce réseau de neurones ?");
			string? sNumberOfOutput = Console.ReadLine();
			if (!uint.TryParse(sNumberOfOutput, out _numberOutput))
				throw new WrongInputException("Nombre de sorties impossible à parser");

			Console.WriteLine("Combien de couche cachés sont dans ce réseau de neurones ?");
			string? sNumberOfHiddenLayers = Console.ReadLine();
			if (!uint.TryParse(sNumberOfHiddenLayers, out numberOfHiddenLayers))
				throw new WrongInputException("Nombre de couche cachés impossible à parser");

			Console.WriteLine("Combien de neurones par couche cachés sont dans ce réseau de neurones ?");
			string? sNumberOfNeuronesByHiddenLayer = Console.ReadLine();
			if (!uint.TryParse(sNumberOfNeuronesByHiddenLayer, out numberOfNeuronesByHiddenLayer))
				throw new WrongInputException("Nombre de neurones par couche cachés impossible à parser");

			Console.WriteLine("Quel sont les type de neurones de ce réseau de neurones");
			Console.WriteLine("     1. Elu");
			Console.WriteLine("     2. LeakyRelu");
			Console.WriteLine("     3. ReLu");
			Console.WriteLine("     4. SeLu");
			Console.WriteLine("     5. Sigmoid");
			Console.WriteLine("     6. Swish");
			Console.WriteLine("     7. Tanh");
			string? sNeuronType = Console.ReadLine();
			if (!int.TryParse(sNeuronType, out int parsedNeuronType))
				throw new WrongInputException("Type de neurones impossible à parser");
			typeOfNeurons = parsedNeuronType switch
			{
				1 => NeuronType.Elu,
				2 => NeuronType.LeakyRelu,
				3 => NeuronType.Relu,
				4 => NeuronType.SeLu,
				5 => NeuronType.Sigmoid,
				6 => NeuronType.Swish,
				7 => NeuronType.Tanh,
				_ => throw new WrongInputException("Ce type de neurone n'existe pas")
			};
			_NeuralNetworkBuilder = new NeuralNetworkBuilder();
			_NeuralNetworkDirector.NetworkBuilder = _NeuralNetworkBuilder;
			_NeuralNetworkDirector.BuildComplexeNeuralNetwork(_numberOfInput, learnRate, momentum,_numberOutput, numberOfHiddenLayers, numberOfNeuronesByHiddenLayer, typeOfNeurons);
			NeuralNetwork = _NeuralNetworkBuilder.GetNeuralNetwork();
			Console.WriteLine("Réseau de neurones crée avec succès !");
			Thread.Sleep(2000);
			Console.WriteLine("-----------------------------------");

		}
	}
}
