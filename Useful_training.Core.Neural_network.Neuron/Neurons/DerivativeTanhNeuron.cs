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
    internal class DerivativeTanhNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            return Math.Pow(Math.Tanh(GetInterpolationResult(input)), 2);
        }
    }
}
