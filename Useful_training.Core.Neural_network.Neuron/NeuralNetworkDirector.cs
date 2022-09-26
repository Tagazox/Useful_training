using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network
{
    public class NeuralNetworkDirector
    {
        private INeuralNetworkBuilder _networkBuilder;
        public INeuralNetworkBuilder networkBuilder { set => _networkBuilder = value; }
        public NeuralNetworkDirector()
        {

        }
        public void BuildMinimalNeuralNetwork(uint numberOfOutputs, NeuronType typeOfNeurons)
        {
            if (_networkBuilder == null)
                throw new BuilderNotDefinedException("Builder need to be defined first");
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
        public void BuildComplexeNeuralNetwork(uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
        {
            if (_networkBuilder == null)
                throw new BuilderNotDefinedException("Builder need to be defined first");
            _networkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayer, numberOfHiddenLayers, typeOfNeurons);
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
        public void BuildComplexeNeuralNetwork(uint numberOfOutputs, uint numberOfHiddenLayers, List<uint> numbersOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
        {
            if (_networkBuilder == null)
                throw new BuilderNotDefinedException("Builder need to be defined first");
            _networkBuilder.AddHiddenLayers(numbersOfNeuronesByHiddenLayer, numberOfHiddenLayers, typeOfNeurons);
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
    }
}
