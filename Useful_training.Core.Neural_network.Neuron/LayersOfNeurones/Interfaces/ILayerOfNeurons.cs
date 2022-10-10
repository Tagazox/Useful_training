using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.Neural_NetworkTests")]
namespace Useful_training.Core.NeuralNetwork.LayersOfNeurones.Interfaces;

internal interface ILayerOfNeurons : ILayerOfInputNeurons, ISerializable
{
    public IList<double> Outputs { get;  }
    public IList<INeuron> Neurons { get; }
    // ReSharper disable once UnusedMemberInSuper.Global
    public new ILayerOfNeurons Clone();
    public void Initialize(uint numberOfNeuron, NeuronType neuronType, ILayerOfInputNeurons inputLayer);
    public void UpdateWeights(double learnRate, double momentum);
    public void CalculateGradiant(List<double>? targets=null);
    public void Calculate();
    public void Reset();
}