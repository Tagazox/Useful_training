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

        public NeuralNetworkBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            Neural_Network = new Neural_Network();
        }

        public void AddInputLayer(uint numberOfInputs, uint numberOfNeurones, NeuronType typeOfNeurons)
        {
            Neural_Network.AddLayer(numberOfInputs, numberOfNeurones, typeOfNeurons);
        }
        public void AddHiddenLayers(uint numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons)
        {
            if (Neural_Network._layersOfNeurons.Count == 0)
                throw new YouNeedToCreateInputLayerFirstException("Hidden layers can't be created alone.");
            for (int i = 0; i < numberOfHiddenLayers; i++)
                Neural_Network.AddLayer((uint)Neural_Network._layersOfNeurons.Last().neurons.Count, numberOfNeuronesBylayers, typeOfNeurons);
        }
        public void AddHiddenLayers(List<uint> numberOfNeuronesBylayers, uint numberOfHiddenLayers, NeuronType typeOfNeurons)
        {
            if (Neural_Network._layersOfNeurons.Count == 0)
                throw new YouNeedToCreateInputLayerFirstException("Hidden layers can't be created alone.");
            if (numberOfNeuronesBylayers.Count != numberOfHiddenLayers)
                throw new ArgumentException("numberOfNeuronesBylayers arrays need to be equal as the number of hidden layers.");

            for (int i = 0; i < numberOfHiddenLayers; i++)
                Neural_Network.AddLayer((uint)Neural_Network._layersOfNeurons.Last().neurons.Count, numberOfNeuronesBylayers[i], typeOfNeurons);
        }
        public void AddOutputLayers(uint numberOfOutputs, NeuronType typeOfNeurons)
        {
            if (Neural_Network._layersOfNeurons.Count == 0)
                throw new YouNeedToCreateInputLayerFirstException("Outputs layers can't be created alone.");
            Neural_Network.AddLayer((uint)Neural_Network._layersOfNeurons.Last().neurons.Count, numberOfOutputs, typeOfNeurons);
        }

        public Neural_Network GetNeural_Network()
        {
            return Neural_Network;
        }

    }
}
