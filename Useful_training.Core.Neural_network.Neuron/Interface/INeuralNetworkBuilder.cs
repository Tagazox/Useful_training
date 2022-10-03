
namespace Useful_training.Core.Neural_network
{
    public interface INeuralNetworkBuilder
    {
        void Initialize(uint numberOfInput, double? learnRate = null, double? momentum = null);
        void AddHiddenLayers(List<uint> numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
        void AddHiddenLayers(uint numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
        void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons);
        NeuralNetwork GetNeural_Network();
        void Reset();
    }
}