using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.Neurons;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neural_NetworkTests")]
namespace Useful_training.Core.Neural_network
{
    [Serializable]
    internal class LayerOfNeurons : ILayerOfNeurons
    {
        private IList<INeuron> _Neurons { get; set; }
		IList<double> ILayerOfNeurons.Outputs { get => _Neurons.Select(n => n.OutputResult).ToList(); }
        IList<INeuron> ILayerOfNeurons.Neurons { get => _Neurons; set => _Neurons = value; }
		IEnumerable<IInputNeurons> ILayerOfInputNeurons.InputNeurons { get => _Neurons.Select(n => (IInputNeurons) n);  }

		public LayerOfNeurons()
        {
        }
        public void Initialize(uint numberOfNeuron, NeuronType neuronType, ILayerOfInputNeurons inputLayer)
        {
            if (numberOfNeuron == 0)
                throw new CantInitializeWithZeroNeuronException("NumberOfNeuron need to be greater than 0, you can't create a layer with 0 neurons");
            if(inputLayer==null || inputLayer.InputNeurons.Count() == 0)
                throw new BadInputLayerException("Input Layer cannot be null or have 0 neurons");
            _Neurons = new List<INeuron>();

            for (int i = 0; i < numberOfNeuron; i++)
            {
                INeuron neuron = neuronType switch
                {
                    NeuronType.Elu => new EluNeuron(inputLayer.InputNeurons),
                    NeuronType.LeakyRelu => new LeakyReLuNeuron(inputLayer.InputNeurons),
                    NeuronType.Relu => new ReLuNeuron(inputLayer.InputNeurons),
                    NeuronType.Selu => new SeLuNeuron(inputLayer.InputNeurons),
                    NeuronType.Sigmoid => new SigmoidNeuron(inputLayer.InputNeurons),
                    NeuronType.Swish => new SwishNeuron(inputLayer.InputNeurons),
                    NeuronType.Tanh => new TanhNeuron(inputLayer.InputNeurons),
                    _ => throw new NeuronTypeDontExsistException("This neuron type don't exist"),
                };
                _Neurons.Add(neuron);
            }
        }
        public ILayerOfNeurons Clone()
        {
            return (ILayerOfNeurons)this.MemberwiseClone();
        }
        ILayerOfInputNeurons ILayerOfInputNeurons.Clone()
		{
            return (ILayerOfInputNeurons)this.MemberwiseClone();
        }
		public void Calculate()
		{
			foreach (INeuron neuron in _Neurons)
                neuron.GetCalculationResult();
		}

		public void CalculateGradiant(List<double> targets = null)
		{
            int targetCounter = 0;

            if (targets == null)
                foreach (INeuron neuron in _Neurons)
                    neuron.CalculateGradient();
            else
            {
                if (targets.Count != _Neurons.Count())
                    throw new ArgumentException("if target is defined it should have the same count as the neurones count");
                foreach (INeuron neuron in _Neurons)
                    neuron.CalculateGradient(targets[targetCounter++]);
            }
        }
        public void UpdateWeights(double learnRate, double momentum)
        {
                foreach (INeuron neuron in _Neurons)
                    neuron.UpdateWeights(learnRate, momentum);
        }
        #region serialization
        protected LayerOfNeurons(SerializationInfo info, StreamingContext context)
        {
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("NeuronList", _Neurons.ToArray());
        }
        #endregion
    }
}
