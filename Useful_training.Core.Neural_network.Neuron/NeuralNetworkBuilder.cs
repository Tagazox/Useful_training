using Useful_training.Core.NeuralNetwork.Interfaces;

namespace Useful_training.Core.NeuralNetwork
{
    public class NeuralNetworkBuilder : INeuralNetworkBuilder
    {
        private NeuralNetwork NeuralNetwork;

        public NeuralNetworkBuilder()
        {
            Reset();
        }
        public void Reset()
        {
            NeuralNetwork = new NeuralNetwork();
        }
        public void Initialize(uint numberOfInput, double? learnRate = null, double? momentum = null)
        {
            NeuralNetwork.Initialize(numberOfInput, learnRate, momentum);
        }
        public void AddHiddenLayers(uint numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons)
        {
            for (int i = 0; i < numberOfHiddenLayers; i++)
                NeuralNetwork.AddHiddenLayer(numberOfNeuronesBylayers, typeOfNeurons);
        }
        public void AddHiddenLayers(List<uint> numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons)
        {
            if (numberOfNeuronesBylayers.Count != numberOfHiddenLayers)
                throw new ArgumentException("count of numberOfNeuronesBylayers arrays need to be equal as the number of hidden layers.");

            for (int i = 0; i < numberOfHiddenLayers; i++)
                NeuralNetwork.AddHiddenLayer(numberOfNeuronesBylayers[i], typeOfNeurons);
        }
        public void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons)
        {
            if (numberOfOutputs<=0)
                throw new ArgumentException("You can't build a neural network withou any outputs.");
            NeuralNetwork.AddHiddenLayer(numberOfOutputs, typeOfNeurons);
        }
        public NeuralNetwork GetNeuralNetwork()
        {
            return NeuralNetwork;
        }

	}
}
