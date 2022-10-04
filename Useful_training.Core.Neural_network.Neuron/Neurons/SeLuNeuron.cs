using Useful_training.Core.NeuralNetwork.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
{
	[Serializable]
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
        #region serialization
        protected override NeuronType GetNeuronType()
        {
            return NeuronType.Selu;
        }
        #endregion
    }
}
