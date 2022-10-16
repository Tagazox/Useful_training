using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Type;

[Serializable]
internal class SeLuNeuron : Neuron
{
    public SeLuNeuron(IEnumerable<IInputNeuron> inputNeurons) : base(inputNeurons)
    {
    }
    public override void GetCalculationResult()
    {
        double interpolationResult = GetInterpolationResult();
        if (interpolationResult >= 0)
            OutputResult = interpolationResult;
        else
            OutputResult = Math.Exp(interpolationResult) - 1;
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