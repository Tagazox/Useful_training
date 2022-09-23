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
    internal class LeakyReLuNeuron : Neuron
    {
        public LeakyReLuNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
        {
        }
        public override void GetCalculationResult()
        {
            var result = GetInterpolationResult();
            OutputResult = Math.Max(0.1 * result, result);
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return ((double)OutputResult) >= 0 ? 1 : 0.01;
        }
    }
}
