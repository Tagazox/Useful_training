using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network;

namespace ConsoleAdapter
{
    internal class ContainerAdapter : INeuralNetworkContainer
    {
        private Neural_Network _neural_Network;
        private NeuralNetworkDirector _neural_NetworkDirector;
        public ContainerAdapter()
        {
            _neural_NetworkDirector = new NeuralNetworkDirector();
        }
        public Neural_Network CreateNeuralNetwork(uint numberOfInputs, uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
        {
            throw new NotImplementedException();
        }

        public Neural_Network GetNeuralNetwork()
        {
            throw new NotImplementedException();
        }
    }
}
