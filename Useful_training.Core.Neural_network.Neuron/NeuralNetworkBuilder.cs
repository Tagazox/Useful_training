using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network
{
    internal class NeuralNetworkBuilder : INeuralNetworkBuilder
    {
        private Neural_Network Neural_Network;
        private uint NumberOfInput;
        private double? LearnRate, Momentum;
        public NeuralNetworkBuilder(uint numberOfInput, double? learnRate = null, double? momentum = null)
        {
            NumberOfInput = numberOfInput;
            LearnRate = learnRate;
            Momentum = momentum;
            Reset();
        }

        public void Reset()
        {
            Neural_Network = new Neural_Network(NumberOfInput, LearnRate, Momentum);
        }

        public void AddHiddenLayers(uint numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons)
        {
            for (int i = 0; i < numberOfHiddenLayers; i++)
                Neural_Network.AddHiddenLayer(numberOfNeuronesBylayers, typeOfNeurons);
        }
        public void AddHiddenLayers(List<uint> numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons)
        {
            if (numberOfNeuronesBylayers.Count != numberOfHiddenLayers)
                throw new ArgumentException("count of numberOfNeuronesBylayers arrays need to be equal as the number of hidden layers.");

            for (int i = 0; i < numberOfHiddenLayers; i++)
                Neural_Network.AddHiddenLayer(numberOfNeuronesBylayers[i], typeOfNeurons);
        }
        public void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons)
        {
            if (numberOfOutputs<=0)
                throw new ArgumentException("You can't build a neural network withou any outputs.");
            Neural_Network.AddHiddenLayer(numberOfOutputs, typeOfNeurons);
        }

        public Neural_Network GetNeural_Network()
        {
            return Neural_Network;
        }

    }
}
