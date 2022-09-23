﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neural_NetworkTests")]
namespace Useful_training.Core.Neural_network
{
	public class Neural_Network
	{
		private readonly ILayerOfInputNeurons InputLayer;
		private IList<ILayerOfNeurons> LayersOfNeurons { get; set; }
		private double LearnRate { get; set; }
		private double Momentum { get; set; }
		public Neural_Network(uint _numberOfInput, double? learnRate, double? momentum)
		{
			LearnRate = learnRate ?? .05;
			Momentum = momentum ?? .9;
			LayersOfNeurons = new List<ILayerOfNeurons>();
			InputLayer = new LayerOfInputNeurons(_numberOfInput);
		}

		internal void AddLayer(uint _numberOfNeuron, NeuronType typeOfNeurons)
		{
			LayerOfNeurons layerOfNeurons = new LayerOfNeurons();
			if (LayersOfNeurons.Count == 0)
				layerOfNeurons.Initialize(_numberOfNeuron, typeOfNeurons, InputLayer);
			else
				layerOfNeurons.Initialize(_numberOfNeuron, typeOfNeurons, LayersOfNeurons.Last());
			LayersOfNeurons.Add(layerOfNeurons);
		}
		public IList<double> Calculate(IList<double> inputs)
		{
			if (inputs == null || inputs.Count == 0 || InputLayer.InputNeurons.Count() != inputs.Count)
				throw new WrongInputForCalculationException("Inputs for the calculation need to be equal as the input number specified at the creation of the neural network");

			int inputCounter = 0;
			foreach (IInputNeurons inputNeurons in InputLayer.InputNeurons)
				inputNeurons.OutputResult = inputs[inputCounter++];

			foreach (ILayerOfNeurons layerOfNeurons in LayersOfNeurons)
				layerOfNeurons.Calculate();

			return LayersOfNeurons.Last().Outputs;
		}
		public void BackPropagate(List<double> targets)
		{
			LayersOfNeurons = LayersOfNeurons.Reverse().ToList();

			ILayerOfNeurons outputLayer = LayersOfNeurons.First();
			List<ILayerOfNeurons> hiddenLayers = LayersOfNeurons.Skip(1).ToList();

			if (outputLayer.Neurons.Count != targets.Count)
				throw new ArgumentException("Targets need to have the same count as the outputs layer number of neurones");

			outputLayer.CalculateGradiant(targets);
			foreach (ILayerOfNeurons layers in hiddenLayers)
			{
				layers.CalculateGradiant();
				layers.UpdateWeights(LearnRate, Momentum);
			}
			outputLayer.UpdateWeights(LearnRate, Momentum);

			LayersOfNeurons = LayersOfNeurons.Reverse().ToList();
		}
	}
}
