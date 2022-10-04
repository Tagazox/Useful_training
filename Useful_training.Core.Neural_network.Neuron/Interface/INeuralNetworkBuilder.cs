
namespace Useful_training.Core.Neural_network
{
    public interface INeuralNetworkBuilder
    {
        internal void Initialize(uint numberOfInput, double? learnRate = null, double? momentum = null);
        internal void AddHiddenLayers(List<uint> numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
        internal void AddHiddenLayers(uint numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
        internal  void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons);
        NeuralNetwork GetNeural_Network();
        void Reset();
    }
}