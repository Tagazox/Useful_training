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
    internal class SeLuNeuron : Neuron
    {
        public SeLuNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
        {
        }
        public override void GetCalculationResult()
        {
            var result = GetInterpolationResult();
            if (result >= 0)
                OutputResult = result;
            else
                OutputResult = (Math.Exp(result) - 1);
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return ((double)OutputResult) <= 0 ? Math.Exp(OutputResult) : 1;
        }
    }
}
