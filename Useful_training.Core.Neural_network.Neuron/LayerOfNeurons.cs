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
        public IList<INeuron> Neurons { get; set; }
        public IList<double> Outputs { get => Neurons.Select(n => n.OutputResult).ToList(); }
        public IEnumerable<IInputNeurons> InputsNeurons { get => Neurons.Select(n => (IInputNeurons) n);  }

		public LayerOfNeurons()
        {
            Neurons = new List<INeuron>();
        }
        public void Initialize(uint numberOfNeurons, NeuronType neuronType, ILayerOfInputNeurons inputLayer)
        {
            if (numberOfNeurons == 0)
                throw new CantInitializeWithZeroNeuronException("NumberOfNeuron need to be greater than 0, you can't create a layer with 0 neurons");
            if(inputLayer==null || inputLayer.InputsNeurons.Count() == 0)
                throw new BadInputLayerException("Input Layer cannot be null or have 0 neurons");
            Neurons = new List<INeuron>();

            for (int i = 0; i < numberOfNeurons; i++)
            {
                INeuron neuron = neuronType switch
                {
                    NeuronType.Elu => new EluNeuron(inputLayer.InputsNeurons),
                    NeuronType.LeakyRelu => new LeakyReLuNeuron(inputLayer.InputsNeurons),
                    NeuronType.Relu => new ReLuNeuron(inputLayer.InputsNeurons),
                    NeuronType.Selu => new SeLuNeuron(inputLayer.InputsNeurons),
                    NeuronType.Sigmoid => new SigmoidNeuron(inputLayer.InputsNeurons),
                    NeuronType.Swish => new SwishNeuron(inputLayer.InputsNeurons),
                    NeuronType.Tanh => new TanhNeuron(inputLayer.InputsNeurons),
                    _ => throw new NeuronTypeDontExsistException("This neuron type don't exist"),
                };
                Neurons.Add(neuron);
            }
        }
        public ILayerOfNeurons Clone()
        {
            return (ILayerOfNeurons)this.MemberwiseClone();
        }
        public void Reset()
        {
            foreach (INeuron neuron in Neurons)
            {
                neuron.Reset();
            };
        }
        ILayerOfInputNeurons ILayerOfInputNeurons.Clone()
		{
            return (ILayerOfInputNeurons)this.MemberwiseClone();
        }
		public void Calculate()
		{
			foreach (INeuron neuron in Neurons)
                neuron.GetCalculationResult();
		}

		public void CalculateGradiant(List<double> targets = null)
		{
            int targetCounter = 0;

            if (targets == null)
                foreach (INeuron neuron in Neurons)
                    neuron.CalculateGradient();
            else
            {
                if (targets.Count != Neurons.Count())
                    throw new ArgumentException("if target is defined it should have the same count as the neurones count");
                foreach (INeuron neuron in Neurons)
                    neuron.CalculateGradient(targets[targetCounter++]);
            }
        }
        public void UpdateWeights(double learnRate, double momentum)
        {
                foreach (INeuron neuron in Neurons)
                    neuron.UpdateWeights(learnRate, momentum);
        }
        #region serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("NeuronList", Neurons.ToArray());
        }
        #endregion
    }
}
