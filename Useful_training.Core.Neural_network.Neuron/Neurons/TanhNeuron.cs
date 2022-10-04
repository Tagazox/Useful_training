using Useful_training.Core.NeuralNetwork.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
{
	[Serializable]
    internal class TanhNeuron : Neuron
    {
        public TanhNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
        {
        }
        public override void GetCalculationResult()
        {
            OutputResult = Math.Tanh(GetInterpolationResult());
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return (Math.Exp(-OutputResult) * (OutputResult + 1) + 1) / (Math.Pow(1 + Math.Exp(-OutputResult), 2));
        }

        #region serialization
        protected override NeuronType GetNeuronType()
        {
            return NeuronType.Tanh;
        }
        #endregion
    }
}
