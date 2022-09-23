using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network
{
	internal abstract class Neuron : INeuron
	{
		public double Gradiant { get; set; }
		public double OutputResult { get; set; }
		protected double bias = 1;
		protected double biasDelta;
		public List<Synapse> OutputSynapses { get; set; }
		protected List<Synapse> InputSynapses { get; set; }
		public abstract void GetCalculationResult();
		internal abstract double DerivativeFunctionResultCalculation();
		public Neuron(IEnumerable<IInputNeurons> inputNeurons)
		{
			if(inputNeurons == null || inputNeurons.Count() == 0)
				throw new CantInitializeWithZeroInputException("Neurones need to be initialize with at leat one input");
			InputSynapses = new List<Synapse>();
			OutputSynapses = new List<Synapse>();
			biasDelta = 0;
			foreach (var inputNeuron in inputNeurons)
			{
				var synapse = new Synapse(inputNeuron, this);
				inputNeuron.OutputSynapses.Add(synapse);
				InputSynapses.Add(synapse);
			}
		}	
		public INeuron Clone()
		{
			return (INeuron)this.MemberwiseClone();
		}
		internal double GetInterpolationResult()
		{
			return InputSynapses.Sum(a => a.Weight * a.InputNeuron.OutputResult) + bias;
		}
		public void UpdateWeights(double learnRate, double momentum)
		{
			var prevDelta = biasDelta;
			biasDelta = learnRate * Gradiant;
			bias += biasDelta + momentum * prevDelta;

			foreach (var synapse in InputSynapses)
			{
				prevDelta = synapse.WeightDelta;
				synapse.WeightDelta = learnRate * Gradiant * synapse.InputNeuron.OutputResult;
				synapse.Weight += synapse.WeightDelta + momentum * prevDelta;
			}
		}
		public double CalculateError(double target)
		{
			return target - OutputResult;
		}
		public double CalculateGradient(double? target = null)
		{
			if (target == null)
				return Gradiant = OutputSynapses.Sum(a => a.OutputNeuron.Gradiant * a.Weight) * DerivativeFunctionResultCalculation();

			return Gradiant = CalculateError(target.Value) * DerivativeFunctionResultCalculation();
		}

		IInputNeurons IInputNeurons.Clone()
		{
			return (IInputNeurons)this.MemberwiseClone();
		}
	}
	
}
