using Useful_training.Core.NeuralNetwork.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons
{
	[Serializable]
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
        #region serialization
        protected override NeuronType GetNeuronType()
        {
            return NeuronType.LeakyRelu;
        }
        #endregion
    }
}
