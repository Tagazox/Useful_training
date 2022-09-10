﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neuron.Exceptions;
using Useful_training.Core.Neural_network.Neuron.Interface;
using Useful_training.Core.Neural_network.Neuron.Neurons;

namespace Useful_training.Core.Neural_network.Neuron
{
    public class LayerOfNeurons : ILayerOfNeurons
    {
        protected IList<INeuron> _neurons { get; set; }
        public LayerOfNeurons()
        {
            _neurons = new List<INeuron>();
        }
        public IList<double> Calculate(IList<double> inputs)
        {
            IList<double> outputs = new List<double>();
            foreach (var neuron in _neurons)
                outputs.Add(neuron.GetCalculationResult(inputs));
            return outputs;
        }
        public ILayerOfNeurons Clone()
        {
            return (ILayerOfNeurons)this.MemberwiseClone();
        }
        public void Initialize(uint numberOfNeuron, int numberOfInput, NeuronType neuronType)
        {
            if (numberOfNeuron == 0)
                throw new CantInitialiseWithZeroNeuronException("NumberOfNeuron need to be greater than 0");
            
            for (int i = 0; i < numberOfNeuron; i++)
            {
                INeuron neuron = neuronType switch
                {
                    NeuronType.Binary => new BinaryNeuron(),
                    NeuronType.DerivativeLeakyRelu => new DerivativeLeakyReLuNeuron(),
                    NeuronType.DerivativeSigmoid => new DerivativeSigmoidNeuron(),
                    NeuronType.DerivativeTanh => new DerivativeTanhNeuron(),
                    NeuronType.Elu => new EluNeuron(),
                    NeuronType.Gelu => new GeLuNeuron(),
                    NeuronType.LeakyRelu => new LeakyReLuNeuron(),
                    NeuronType.Linear => new LinearNeuron(),
                    NeuronType.Relu => new ReLuNeuron(),
                    NeuronType.Selu => new SeLuNeuron(),
                    NeuronType.Sigmoid => new SigmoidNeuron(),
                    NeuronType.Swish => new SwishNeuron(),
                    NeuronType.Tanh => new TanhNeuron(),
                    _ => throw new Exception("This neuron type don't exist"),
                };
                neuron.InitialiseWithRandomValues(numberOfInput);
                _neurons.Add(neuron);
            }
        }

    }
}
