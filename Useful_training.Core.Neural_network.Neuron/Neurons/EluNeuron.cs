using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neuron.Interface;

namespace Useful_training.Core.Neural_network.Neuron.Neurons
{
    public class EluNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            var result = GetCalculationResult(input);
            return result >=0 ? result : (Math.Exp(result)-1);
        }
    }
}
