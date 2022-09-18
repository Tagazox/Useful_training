using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.Neurons;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.LayerOfNeuronsTests")]
namespace Useful_training.Core.Neural_network
{
    internal class LayerOfNeurons : ILayerOfNeurons
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
        public void Initialize(uint numberOfNeuron, uint numberOfInput, NeuronType neuronType)
        {
            if (numberOfNeuron == 0)
                throw new CantInitializeWithZeroNeuronException("NumberOfNeuron need to be greater than 0");
            
            for (int i = 0; i < numberOfNeuron; i++)
            {
                INeuron neuron = neuronType switch
                {
                    NeuronType.Elu => new EluNeuron(),
                    NeuronType.LeakyRelu => new LeakyReLuNeuron(),
                    NeuronType.Relu => new ReLuNeuron(),
                    NeuronType.Selu => new SeLuNeuron(),
                    NeuronType.Sigmoid => new SigmoidNeuron(),
                    NeuronType.Swish => new SwishNeuron(),
                    NeuronType.Tanh => new TanhNeuron(),
                    _ => throw new NeuronTypeDontExsistException("This neuron type don't exist"),
                };
                neuron.InitialiseWithRandomValues(numberOfInput);
                _neurons.Add(neuron);
            }
        }
        public IList<IList<double>> BackPropagate(List<double> targets)
        {
            IList<IList<double>> gradiantPropagationsValues = new List<IList<double>>();
            if (targets.Count != _neurons.Count)
            {
                throw new WrongTargetsForBackpropagationCalculationException("Targets need to have the same count of neurones in the layer.");
            }
            for (int i = 0; i < _neurons.Count; i++)
            {
                gradiantPropagationsValues.Add(_neurons[i].UpdateWeights(targets[i]));
            }
            return gradiantPropagationsValues;


        }
    }
}
