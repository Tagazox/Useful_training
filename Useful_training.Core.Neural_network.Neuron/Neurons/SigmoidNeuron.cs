using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Interface;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

[assembly: InternalsVisibleTo("Useful_training.Core.Neural_network.Neuron.Tests")]
namespace Useful_training.Core.Neural_network.Neurons
{
    [Serializable]
    internal class SigmoidNeuron : Neuron
    {
        public SigmoidNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
        {
        }
        public override void GetCalculationResult()
        {
            OutputResult = SigmoidFunction(GetInterpolationResult());
        }
        private double SigmoidFunction(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return SigmoidFunction(OutputResult) * (1 - SigmoidFunction(OutputResult));
        }
        #region serialization
        protected override NeuronType GetNeuronType()
        {
            return NeuronType.Sigmoid;
        }
        #endregion
    }
}
