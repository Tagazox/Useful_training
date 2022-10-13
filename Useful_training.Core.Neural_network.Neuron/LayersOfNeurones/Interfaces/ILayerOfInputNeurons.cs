﻿using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Useful_training.Core.NeuralNetwork.LayersOfNeurones.Interfaces;

internal interface ILayerOfInputNeurons
{
    public IList<IInputNeuron> InputsNeurons { get; }
    public ILayerOfInputNeurons Clone();
}