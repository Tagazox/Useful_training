using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Type;

[Serializable]
internal class LeakyReLuNeuron : Neuron
{
    public LeakyReLuNeuron(IEnumerable<IInputNeuron> inputNeurons) : base(inputNeurons)
    {
    }
    public override void GetCalculationResult()
    {
        double result = GetInterpolationResult();
        OutputResult = Math.Max(0.1 * result, result);
    }
    internal override double DerivativeFunctionResultCalculation()
    {
        return OutputResult >= 0 ? 1 : 0.01;
    }
    #region serialization
    protected override NeuronType GetNeuronType()
    {
        return NeuronType.LeakyRelu;
    }
    #endregion
}