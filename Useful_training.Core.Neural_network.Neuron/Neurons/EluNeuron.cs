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
    internal class EluNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            var result = GetInterpolationResult(input);
            return _outputResult = result >= 0 ? result : (Math.Exp(result)-1);
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return ((double)_outputResult) >= 1 ? 1 : (_outputResult >= 0 ? _outputResult : (Math.Exp(_outputResult) - 1));
        }
    }
}
