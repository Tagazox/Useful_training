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
    internal class ReLuNeuron : Neuron
    {
        public ReLuNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
        {
        }
        public override void GetCalculationResult()
        {
            OutputResult = Math.Max(0, GetInterpolationResult());
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return ((double)OutputResult) >= 0 ? 1 : 0;
        }
    }
}
