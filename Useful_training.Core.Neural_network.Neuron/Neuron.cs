using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network
{
    [Serializable]
    internal abstract class Neuron : INeuron
    {
        public double Gradiant { get; set; }
        public double OutputResult { get; set; }
        protected double bias = 1;
        protected double biasDelta;
        public List<Synapse> OutputSynapses { get; set; }
        protected List<Synapse> InputSynapses { get; set; }
        #region serialization
        public List<double> WeightDelta
        {
            set
            {
                if (value.Count != InputSynapses.Count)
                    throw new ArgumentException("if you set the weight delta you need to have the same count as number neuron");
                for (int i = 0; i < value.Count; i++)
                {
                    InputSynapses[i].WeightDelta = value[i];
                }
            }
        }
        public List<double> Weight
        {
            set
            {
                if (value.Count != InputSynapses.Count)
                    throw new ArgumentException("if you set the weight you need to have the same count as number neuron");
                for (int i = 0; i < value.Count; i++)
                {
                    InputSynapses[i].Weight = value[i];
                }
            }
        }
        public double Bias { set { bias = value; } }
        public double BiasDelta { set { biasDelta = value; } }
        #endregion
        public abstract void GetCalculationResult();
        internal abstract double DerivativeFunctionResultCalculation();
        public Neuron(IEnumerable<IInputNeurons> inputNeurons)
        {
            if (inputNeurons == null || inputNeurons.Count() == 0)
                throw new CantInitializeWithZeroInputException("Neurones need to be initialize with at leat one input");
            InputSynapses = new List<Synapse>();
            OutputSynapses = new List<Synapse>();
            biasDelta = 0;
            foreach (var inputNeuron in inputNeurons)
            {
                Synapse synapse = new Synapse(inputNeuron, this);
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
            if (learnRate <= 0)
                throw new ArgumentException("Learn rate need to be above 0");
            if (momentum <= 0)
                throw new ArgumentException("Momentum need to be above 0");
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
        private double CalculateError(double target)
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
        #region serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Weight", InputSynapses.Select(w => w.Weight));
            info.AddValue("WeightDelta", InputSynapses.Select(w => w.WeightDelta));
            info.AddValue("Bias", bias);
            info.AddValue("BiasDelta", biasDelta);
            info.AddValue("Type", (int)GetNeuronType());
        }
        protected abstract NeuronType GetNeuronType();
        #endregion

    }

}
