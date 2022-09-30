using ConsoleAdapter.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.ValueObject;

namespace ConsoleAdapter
{
	internal class ContainerConsoleAdapter : INeuralNetworkTrainerContainer
	{
		public INeural_Network Neural_Network { get; private set; }
		public List<DataSet> DataSets { get; private set; }
		private uint numberOfInput, numberOutput;
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
					Console.WriteLine($"Entrée (Attente de {numberOfInput} entrée(s) séparés par un espace):");
					string? inputs = Console.ReadLine();
					string[] inputsArray = inputs.Split(' ');
					if (inputsArray.Length == numberOfInput)
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
					Console.WriteLine($"Sorties (Attente de {numberOutput} sorties(s) séparés par un espace):");
					string? outputs = Console.ReadLine();
					string[] outputsArray = outputs.Split(' ');
					if (outputsArray.Length == numberOutput)
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

			NeuralNetworkDirector _neural_NetworkDirector = new NeuralNetworkDirector();
			NeuralNetworkBuilder _neural_NetworkBuilder;
			Console.Clear();
			Console.WriteLine("-----------------------------------");
			Console.WriteLine("Bienvenue dans le créateur de réseau de neurones");

			Console.WriteLine("Combien d'entrées doivent être géré par ce réseau de neurones ?");
			string? sNumberOfInput = Console.ReadLine();
			if (!uint.TryParse(sNumberOfInput, out numberOfInput))
				throw new WrongInputException("Nombre d'entrées impossible à parser");

			Console.WriteLine("Combien quel est le learn rate ?");
			string? sLearnRate = Console.ReadLine();
			if (!double.TryParse(sLearnRate, NumberStyles.Any, CultureInfo.InvariantCulture, out learnRate))
				throw new WrongInputException("Learn rate impossible à parser");

			Console.WriteLine("Combien quel est le momentum ?");
			string? sMomentum = Console.ReadLine();
			if (!double.TryParse(sMomentum, NumberStyles.Any, CultureInfo.InvariantCulture, out momentum))
				throw new WrongInputException("Momentum impossible à parser");

			_neural_NetworkBuilder = new NeuralNetworkBuilder(numberOfInput, learnRate, momentum);
			_neural_NetworkDirector.networkBuilder = _neural_NetworkBuilder;

			Console.WriteLine("Combien de sorties doivent être géré par ce réseau de neurones ?");
			string? sNumberOfOutput = Console.ReadLine();
			if (!uint.TryParse(sNumberOfOutput, out numberOutput))
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
			int parsedNeuronType;
			if (!int.TryParse(sNeuronType, out parsedNeuronType))
				throw new WrongInputException("Type de neurones impossible à parser");
			switch (parsedNeuronType)
			{
				case 1:
					typeOfNeurons = NeuronType.Elu;
					break;
				case 2:
					typeOfNeurons = NeuronType.LeakyRelu;
					break;
				case 3:
					typeOfNeurons = NeuronType.Relu;
					break;
				case 4:
					typeOfNeurons = NeuronType.Selu;
					break;
				case 5:
					typeOfNeurons = NeuronType.Sigmoid;
					break;
				case 6:
					typeOfNeurons = NeuronType.Swish;
					break;
				case 7:
					typeOfNeurons = NeuronType.Tanh;
					break;

				default:
					throw new WrongInputException("Ce type de neurone n'existe pas");
			}
			_neural_NetworkDirector.BuildComplexeNeuralNetwork(numberOutput, numberOfHiddenLayers, numberOfNeuronesByHiddenLayer, typeOfNeurons);
			Neural_Network = _neural_NetworkBuilder.GetNeural_Network();
			Console.WriteLine("Réseau de neurones crée avec succès !");
			Thread.Sleep(2000);
			Console.WriteLine("-----------------------------------");

		}
	}
}
