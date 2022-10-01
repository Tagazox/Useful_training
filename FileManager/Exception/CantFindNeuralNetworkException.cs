using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Exceptions
{
    public class CantFindNeuralNetworkException : Exception
    {
            public CantFindNeuralNetworkException(string message) : base(message)
            {
            }
    }
}
