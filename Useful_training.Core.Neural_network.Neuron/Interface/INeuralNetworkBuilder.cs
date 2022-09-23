
namespace Useful_training.Core.Neural_network
{
    public interface INeuralNetworkBuilder
    {
        void AddHiddenLayers(List<uint> numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
        void AddHiddenLayers(uint numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
        void AddInputLayer(uint numberOfInputs);
        void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons);
        Neural_Network GetNeural_Network();
        void Reset();
    }
}