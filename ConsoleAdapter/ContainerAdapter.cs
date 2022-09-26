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
        public void CreateNeuralNetwork(uint numberOfInputs, uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
        {
            INeuralNetworkBuilder Builder = new NeuralNetworkBuilder(numberOfInputs);
            _neural_NetworkDirector.networkBuilder = Builder;
            _neural_NetworkDirector.BuildComplexeNeuralNetwork(numberOfOutputs, numberOfHiddenLayers, numberOfNeuronesByHiddenLayer, typeOfNeurons);
            _neural_Network = Builder.GetNeural_Network();
        }

        public Neural_Network GetNeuralNetwork()
        {
            return _neural_Network;
        }
    }
}
