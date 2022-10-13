using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Type;

[Serializable]
internal class SigmoidNeuron : Neuron
{
    public SigmoidNeuron(IEnumerable<IInputNeuron> inputNeurons) : base(inputNeurons)
    {
    }
    public override void GetCalculationResult()
    {
        OutputResult = SigmoidFunction(GetInterpolationResult());
    }
    private static double SigmoidFunction(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }
    internal override double DerivativeFunctionResultCalculation()
    {
        return SigmoidFunction(OutputResult) * (1 - SigmoidFunction(OutputResult));
    }
    #region serialization
    protected override NeuronType GetNeuronType()
    {
        return NeuronType.Sigmoid;
    }
    #endregion
}