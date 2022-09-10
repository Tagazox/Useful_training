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
    internal class LinearNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            return GetInterpolationResult(input);
        }
    }
}
