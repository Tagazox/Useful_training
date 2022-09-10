using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.Neuron.Exceptions
{
    public class CantInitialiseWithZeroNeuronException : Exception
    {
            public CantInitialiseWithZeroNeuronException(string message) : base(message)
            {
            }
    }
}
