using System.Runtime.Serialization;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.Neurons
{
    [Serializable]
    internal abstract class Neuron : INeuron
    {
        public double Gradiant { get; set; }
        public double OutputResult { get; set; }
        public double Bias { protected get; set; }
        public double BiasDelta { protected get; set; }
        List<Synapse> IInputNeurons.OutputSynapses { get; set; } = new List<Synapse>();
        protected List<Synapse> InputSynapses { get; set; } = new List<Synapse>();

        #region serialization
        public List<double> WeightDelta
        {
            set
            {
                if (value.Count != InputSynapses.Count)
                    throw new ArgumentException(
                        "if you set the weight delta you need to have the same count as number neuron");
                for (int i = 0; i < value.Count; i++)
                    InputSynapses[i].WeightDelta = value[i];
            }
        }
        public List<double> Weight
        {
            set
            {
                if (value.Count != InputSynapses.Count)
                    throw new ArgumentException(
                        "if you set the weight you need to have the same count as number neuron");
                for (int i = 0; i < value.Count; i++)
                    InputSynapses[i].Weight = value[i];
            }
        }

        

        #endregion

        public abstract void GetCalculationResult();
        internal abstract double DerivativeFunctionResultCalculation();

        protected Neuron(IEnumerable<IInputNeurons> inputNeurons)
        {
            var inputNeuronsEnumerable = inputNeurons as IInputNeurons[] ?? inputNeurons.ToArray();
            if (inputNeurons == null || !inputNeuronsEnumerable.Any())
                throw new CantInitializeWithZeroInputException("Neuron need to be initialize with at least one input");
            Random random = new Random();
            BiasDelta = random.NextDouble()*2-1;
            Bias = random.NextDouble()*2-1;
            foreach (IInputNeurons inputNeuron in inputNeuronsEnumerable)
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
            return InputSynapses.Sum(a => a.Weight * a.InputNeuron.OutputResult) + Bias;
        }

        public void UpdateWeights(double learnRate, double momentum)
        {
            if (learnRate is <= 0 or > 1)
                throw new ArgumentException("Learn rate need to be between 0 and 1");
            if (momentum is <= 0 or > 1)
                throw new ArgumentException("Momentum need to be between 0 and 1");
            double prevDelta = BiasDelta;
            BiasDelta = learnRate * Gradiant;
            Bias += BiasDelta + momentum * prevDelta;

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
                return Gradiant = ((IInputNeurons)this).OutputSynapses.Sum(a => a.OutputNeuron.Gradiant * a.Weight) *
                                  DerivativeFunctionResultCalculation();

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
            info.AddValue("Bias", Bias);
            info.AddValue("BiasDelta", BiasDelta);
            info.AddValue("Type", (int)GetNeuronType());
        }

        protected abstract NeuronType GetNeuronType();

        public void Reset()
        {
            Random random = new Random();
            foreach (Synapse synapse in InputSynapses)
            {
                Bias = random.NextDouble()*2-1;
                BiasDelta = random.NextDouble()*2-1;
                synapse.Reset();
            }
        }
        #endregion
    }
}