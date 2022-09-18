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
    internal class TanhNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            return _outputResult = Math.Tanh(GetInterpolationResult(input));
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return (Math.Exp(-_outputResult) * (_outputResult + 1) + 1) / (Math.Pow(1 + Math.Exp(-_outputResult), 2));
        }
    }
}
