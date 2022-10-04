using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Interfaces;

namespace Useful_training.Core.NeuralNetwork
{
    public class NeuralNetworkDirector
    {
        private INeuralNetworkBuilder _networkBuilder;
        public INeuralNetworkBuilder networkBuilder { set => _networkBuilder = value; }
        public NeuralNetworkDirector()
        {

        }
        public void BuildMinimalNeuralNetwork(uint numberOfInput, uint numberOfOutputs, NeuronType typeOfNeurons)
        {
            if (_networkBuilder == null)
                throw new BuilderNotDefinedException("Builder need to be defined first");
            _networkBuilder.Initialize(numberOfInput);
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
        public void BuildComplexeNeuralNetwork(uint numberOfInput,double learnRate,double momentum, uint numberOfOutputs, uint numberOfHiddenLayers, uint numberOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
        {
            if (_networkBuilder == null)
                throw new BuilderNotDefinedException("Builder need to be defined first");
            _networkBuilder.Initialize(numberOfInput, learnRate,momentum);
            _networkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayer, numberOfHiddenLayers, typeOfNeurons);
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
        public void BuildComplexeNeuralNetwork(uint numberOfInput, double learnRate, double momentum, uint numberOfOutputs, uint numberOfHiddenLayers, List<uint> numbersOfNeuronesByHiddenLayer, NeuronType typeOfNeurons)
        {
            if (_networkBuilder == null)
                throw new BuilderNotDefinedException("Builder need to be defined first");
            _networkBuilder.Initialize(numberOfInput, learnRate,momentum);
            _networkBuilder.AddHiddenLayers(numbersOfNeuronesByHiddenLayer, numberOfHiddenLayers, typeOfNeurons);
            _networkBuilder.AddOutputLayers(numberOfOutputs, typeOfNeurons);
        }
    }
}
