﻿using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests.Test")]
namespace Useful_training.Core.NeuralNetwork.Interfaces
{
	internal interface IInputNeurons
	{
		internal double OutputResult { get; set; }
		internal List<Synapse> OutputSynapses { get; set; }
		public IInputNeurons Clone();
	}
}
