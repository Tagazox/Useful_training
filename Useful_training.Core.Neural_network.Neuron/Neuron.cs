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
        private int bias = 1;
        protected List<double> _weight;
        static Random _rand = new Random();
        public abstract double GetCalculationResult(IList<double> input);

        public Neuron()
        {
            _weight = new List<double>();
        }
        public INeuron Clone()
        {
            return (INeuron)this.MemberwiseClone();
        }

        public void InitialiseWithRandomValues(int NumberOfInputs)
        {
            if (NumberOfInputs <= 0)
                throw new CantInitialiseWithZeroInputException("Neuron need to be initialise with a positive number, greater than 0");
            NumberOfInputs++;
            for (int i = 0; i < NumberOfInputs; i++)
                _weight.Add(_rand.NextDouble() * 2 - 1);
        }
        protected double GetInterpolationResult(IList<double> input)
        {
            if (_weight.Count == 0)
                throw new NeuronNotInitialisedException("Neuron need to be initialise");
            if (input == null || input.Count+1 != _weight.Count)
                throw new WrongInputForCalculationException("Wrong input, input is null or not equal the count of weight");

            double result = 0;
            for (int i = 0; i < input.Count; i++)
            {
                result += _weight[i] * input[i];
            }
            result += _weight.Last() * bias;
            return result;
        }
    }
}
