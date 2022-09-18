using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;

namespace Useful_training.Core.Neural_network
{
    public abstract class Neuron : INeuron
    {
        private double bias = 1;
        protected double biasDelta;
        protected double _outputResult;
        protected List<double> _weightDelta;
        protected double _learnRate;
        protected double _momentum;
        protected List<double> _weight;
        static Random _rand = new Random();
        public abstract double GetCalculationResult(IList<double> input);
        internal abstract double DerivativeFunctionResultCalculation();

        public Neuron()
        {
            _weight = new List<double>();
            _weightDelta = new List<double>();  
            _learnRate = 0.4;
            _momentum = 0.9;
        }
        public INeuron Clone()
        {
            return (INeuron)this.MemberwiseClone();
        }

        public void InitialiseWithRandomValues(uint NumberOfInputs)
        {
            if (NumberOfInputs <= 0)
                throw new CantInitializeWithZeroInputException("Neuron need to be initialise with a positive number, greater than 0");
            for (int i = 0; i < NumberOfInputs; i++)
                _weight.Add(_rand.NextDouble() * 2 - 1);
            for (int i = 0; i < NumberOfInputs; i++)
                _weightDelta.Add(0);
        }
        public double GetInterpolationResult(IList<double> input)
        {
            if (_weight.Count == 0)
                throw new NeuronNotInitialisedException("Neuron need to be initialise");
            if (input == null || input.Count != _weight.Count)
                throw new WrongInputForCalculationException("Wrong input, input is null or not equal the count of weight");

            double result = 0;
            for (int i = 0; i < input.Count; i++)
            {
                result += _weight[i] * input[i];
            }
            result +=  bias;
            return result;
        }

        public IList<double> UpdateWeights(double target)
        {
            double errorValue = (target)- (_outputResult);
            double Gradient = errorValue * DerivativeFunctionResultCalculation();

            var prevDelta = biasDelta;
            biasDelta = _learnRate * Gradient;
            bias += biasDelta + _momentum * prevDelta;

            for (int i = 0; i < _weight.Count; i++)
            {
                double _previousWeightDelta = _weightDelta[i];
                _weightDelta[i] = _learnRate * Gradient * _weight[i];
                _weight[i] += _weightDelta[i] + _momentum * _previousWeightDelta;
            }
            return _weight.Select(w => w * Gradient).ToList();
        }
    }
}
