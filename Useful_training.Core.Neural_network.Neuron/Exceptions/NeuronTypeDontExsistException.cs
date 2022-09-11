using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network.Exceptions
{
    public class NeuronTypeDontExsistException : Exception
    {
            public NeuronTypeDontExsistException(string message) : base(message)
            {
            }
    }
}
