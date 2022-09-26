using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network
{
    public interface INeuralNetworkContainer
    {
        public Neural_Network GetNeuralNetwork();
        public void CreateNeuralNetwork(uint numberOfInputs, uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronesByHiddenLayer, NeuronType typeOfNeurons);
    }
}
