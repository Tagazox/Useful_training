using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Type;

[Serializable]
internal class SeLuNeuron : Neuron
{
    public SeLuNeuron(IEnumerable<IInputNeurons> inputNeurons) : base(inputNeurons)
    {
    }
    public override void GetCalculationResult()
    {
        double result = GetInterpolationResult();
        if (result >= 0)
            OutputResult = result;
        else
            OutputResult = Math.Exp(result) - 1;
    }
    internal override double DerivativeFunctionResultCalculation()
    {
        return OutputResult <= 0 ? Math.Exp(OutputResult) : 1;
    }
    #region serialization
    protected override NeuronType GetNeuronType()
    {
        return NeuronType.SeLu;
    }
    #endregion
}