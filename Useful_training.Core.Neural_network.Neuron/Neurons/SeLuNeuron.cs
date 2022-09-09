using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Neuron.Interface;

namespace Useful_training.Core.Neural_network.Neuron.Neurons
{
    public class SeLuNeuron : Neuron
    {
        public override double GetCalculationResult(IList<double> input)
        {
            var result = GetCalculationResult(input);
            if(result >=0)
                return result;
            else
                return (Math.Exp(result)-1);
        }
    }
}
