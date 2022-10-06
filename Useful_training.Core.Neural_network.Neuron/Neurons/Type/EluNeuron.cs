using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Type
{
	[Serializable]
    internal class EluNeuron : Neuron
    {
		public EluNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
		{
		}
		public override void GetCalculationResult()
        {
            var result = GetInterpolationResult();
            OutputResult = result >= 0 ? result  : (Math.Exp(result ) -1);
        }
        internal override double DerivativeFunctionResultCalculation()
        {
            return ((double)OutputResult) >= 1 ? 1 : (OutputResult >= 0 ? OutputResult : (Math.Exp(OutputResult) - 1));
        }
        #region serialization
        protected override NeuronType GetNeuronType()
        {
            return NeuronType.Elu;
        }
        #endregion
    }
}
