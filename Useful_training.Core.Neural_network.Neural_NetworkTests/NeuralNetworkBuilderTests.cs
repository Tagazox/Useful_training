using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class NeuralNetworkDirectorTest
    {
        NeuralNetworkDirector testDirector;
        NeuralNetworkBuilder testBuilder;
        Random rand;
        uint  numberOfInputs, numberOfOutputs, numberOfHiddenLayers, numberOfNeuronesByHiddenLayer;
        int enumTypeNeuronMaxNumber;
        List<uint> numbersOfNeuronesByHiddenLayerList;
        public NeuralNetworkDirectorTest()
        {
            testDirector = new NeuralNetworkDirector();
            testBuilder = new NeuralNetworkBuilder();  
            rand = new Random();
            enumTypeNeuronMaxNumber = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>().Count();
            numberOfInputs = (uint)rand.Next(10);
            numberOfOutputs = (uint)rand.Next(10);
            numberOfHiddenLayers = (uint)rand.Next(10);
            numberOfNeuronesByHiddenLayer = (uint)rand.Next(10);
            numbersOfNeuronesByHiddenLayerList = new List<uint>();
        }

        [Fact]
        public void DirectorShouldBuildNeuralNetworkFine()
        {
            testDirector.NetworkBuilder = testBuilder;
            testDirector.BuildMinimalNeuralNetwork(numberOfInputs, numberOfOutputs, (NeuronType) rand.Next(enumTypeNeuronMaxNumber));
            testBuilder.Should().NotBeNull();
        }


        [Fact]
        public void DirectorShouldBuildComplexeNeuralNetworkFine()
        {
            testDirector.NetworkBuilder = testBuilder;
            testDirector.BuildComplexeNeuralNetwork(numberOfInputs, numberOfOutputs, numberOfHiddenLayers, numberOfNeuronesByHiddenLayer, (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            testBuilder.Should().NotBeNull();
        }


        [Fact]
        public void DirectorShouldBuildComplexeNeuralNetworkCaseTwoFine()
        {
            for (int i = 0; i < numberOfHiddenLayers; i++)
                numbersOfNeuronesByHiddenLayerList.Add((uint)rand.Next(1, 10));
            testDirector.NetworkBuilder = testBuilder;
            testDirector.BuildComplexeNeuralNetwork(numberOfInputs, numberOfOutputs, numberOfHiddenLayers, numberOfNeuronesByHiddenLayer, (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            testBuilder.Should().NotBeNull();
        }
    }
}