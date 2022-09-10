using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neuron.Interface;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neuron.Tests")]
namespace Useful_training.Core.Neural_network.Neuron.Neurons
{
    internal class DerivativeSigmoidNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            var SigmoidResult = SigmoidFunction(GetInterpolationResult(input));
            return SigmoidResult * (1.0- SigmoidResult);
        }
        private double SigmoidFunction(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}
