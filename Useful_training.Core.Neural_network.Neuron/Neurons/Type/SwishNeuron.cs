using System.Runtime.CompilerServices;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.NeuralNetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Neurons.Type;

[Serializable]
internal class SwishNeuron : Neuron
{
    public SwishNeuron(IEnumerable<IInputNeuron> inputNeurons) : base(inputNeurons)
    {
    }
    public override void GetCalculationResult()
    {
        double result = GetInterpolationResult();
        OutputResult = result * SigmoidFunction(result);
    }
    private static double SigmoidFunction(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }
    internal override double DerivativeFunctionResultCalculation()
    {
        return (Math.Exp(-OutputResult)*(OutputResult+1)+1)/Math.Pow(1+Math.Exp(-OutputResult),2);
    }
    #region serialization
    protected override NeuronType GetNeuronType()
    {
        return NeuronType.Swish;
    }
    #endregion
}