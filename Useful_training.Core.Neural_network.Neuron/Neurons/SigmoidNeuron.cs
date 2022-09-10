using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neuron.Interface;

namespace Useful_training.Core.Neural_network.Neuron.Neurons
{
    public class SigmoidNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            return 1 / (1 + Math.Exp(-GetInterpolationResult(input)));
        }
    }
}
