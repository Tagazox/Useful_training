using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Type
{
	[Serializable]
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
            return OutputResult >= 0 ? 1 : 0;
        }
        #region serialization
        protected override NeuronType GetNeuronType()
        {
            return NeuronType.Relu;
        }
        #endregion
    }
}
