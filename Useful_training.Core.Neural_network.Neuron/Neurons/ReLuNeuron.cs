using Useful_training.Core.NeuralNetwork.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
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
            return ((double)OutputResult) >= 0 ? 1 : 0;
        }
        #region serialization
        protected override NeuronType GetNeuronType()
        {
            return NeuronType.Relu;
        }
        #endregion
    }
}
