using Useful_training.Core.NeuralNetwork.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
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
