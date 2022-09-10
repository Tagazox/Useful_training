﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neuron.Neurons;

namespace Useful_training.Core.Neural_network.Neuron.Interface
{
    public interface ILayerOfNeurons
    {
        public ILayerOfNeurons Clone();
        public void Initialize(uint numberOfNeuron,int numberOfInput, NeuronType neuronType);
        public IList<double> Calculate(IList<double> inputs);
    }
}
