namespace Useful_training.Core.NeuralNetwork.Interfaces
{
    public interface INeuralNetworkBuilder
    {
        internal void Initialize(uint numberOfInput, double? learnRate = null, double? momentum = null);
        internal void AddHiddenLayers(List<uint> numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
        internal void AddHiddenLayers(uint numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons);
        internal  void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons);
        public NeuralNetwork GetNeuralNetwork();
        public void Reset();
    }
}