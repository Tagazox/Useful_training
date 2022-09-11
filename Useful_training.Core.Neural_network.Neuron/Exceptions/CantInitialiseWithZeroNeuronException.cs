using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.Exceptions
{
    public class CantInitializeWithZeroNeuronException : Exception
    {
            public CantInitializeWithZeroNeuronException(string message) : base(message)
            {
            }
    }
}
