using Useful_training.Core.NeuralNetwork.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
{
	[Serializable]
    internal class SwishNeuron : Neuron
    {
        public SwishNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
        {
        }
        public override void GetCalculationResult()
        {
            var result = GetInterpolationResult();
            OutputResult = result * SigmoidFunction(result);
        }
        private double SigmoidFunction(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return (Math.Exp(-OutputResult)*(OutputResult+1)+1)/(Math.Pow(1+Math.Exp(-OutputResult),2));
        }
        #region serialization
        protected override NeuronType GetNeuronType()
        {
            return NeuronType.Swish;
        }
        #endregion
    }
}
