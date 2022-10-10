using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Type;

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
        return (Math.Exp(-OutputResult) * (OutputResult + 1) + 1) / Math.Pow(1 + Math.Exp(-OutputResult), 2);
    }

    #region serialization
    protected override NeuronType GetNeuronType()
    {
        return NeuronType.Tanh;
    }
    #endregion
}