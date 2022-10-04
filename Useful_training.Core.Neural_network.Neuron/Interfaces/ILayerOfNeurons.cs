using System.Runtime.Serialization;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Useful_training.Core.NeuralNetwork.Neural_NetworkTests")]
namespace Useful_training.Core.NeuralNetwork.Interfaces
{
	internal interface ILayerOfNeurons : ILayerOfInputNeurons, ISerializable
    {
        internal IList<double> Outputs { get;  }
        internal IList<INeuron> Neurons { get; set; }
        public new ILayerOfNeurons Clone();
        internal void Initialize(uint numberOfNeuron, NeuronType neuronType, ILayerOfInputNeurons inputLayer);
        internal void UpdateWeights(double learnRate, double momentum);
        internal void CalculateGradiant(List<double> targets=null);
        internal void Calculate();
        internal void Reset();
    }
}
