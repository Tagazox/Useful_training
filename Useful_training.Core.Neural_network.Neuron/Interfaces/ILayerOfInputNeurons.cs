using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Useful_training.Core.NeuralNetwork.Interfaces
{
	internal interface ILayerOfInputNeurons
    {
        internal IEnumerable<IInputNeurons> InputsNeurons { get; }
        public ILayerOfInputNeurons Clone();
    }
}
