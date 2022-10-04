using Useful_training.Core.NeuralNetwork.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
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
