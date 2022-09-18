using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Interface;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neuron.Tests")]
namespace Useful_training.Core.Neural_network.Neurons
{
    internal class SigmoidNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            _outputResult = SigmoidFunction(GetInterpolationResult(input));
         
            return _outputResult;
        }
        private double SigmoidFunction(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return SigmoidFunction(_outputResult) * (1 - SigmoidFunction(_outputResult));
        }
    }
}
