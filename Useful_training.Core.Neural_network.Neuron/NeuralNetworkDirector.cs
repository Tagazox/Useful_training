using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Useful_training.Core.Neural_network
{
    public class NeuralNetworkDirector
    {
        private INeuralNetworkBuilder _networkBuilder;
        public INeuralNetworkBuilder NetworkBuilder { set { _networkBuilder = value; } }
        public NeuralNetworkDirector()
        {

        }
        public void BuildMinimalNeuralNetwork(uint numberOfInputs, uint numberOfOutputs, NeuronType typeOfNeurons)
        {
            _networkBuilder.AddInputLayer(numberOfInputs, 2, typeOfNeurons);
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
        public void BuildComplexeNeuralNetwork(uint numberOfInputs, uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
        {
            _networkBuilder.AddInputLayer(numberOfInputs, 2, typeOfNeurons);
            _networkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayer, numberOfHiddenLayers, typeOfNeurons);
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
        public void BuildComplexeNeuralNetwork(uint numberOfInputs, uint numberOfOutputs, uint numberOfHiddenLayers, List<uint> numbersOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
        {
            _networkBuilder.AddInputLayer(numberOfInputs, 2, typeOfNeurons);
            _networkBuilder.AddHiddenLayers(numbersOfNeuronesByHiddenLayer, numberOfHiddenLayers, typeOfNeurons);
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
    }
}
