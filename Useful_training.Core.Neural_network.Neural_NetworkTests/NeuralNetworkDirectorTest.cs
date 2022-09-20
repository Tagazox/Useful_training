using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class NeuralNetworkBuilderTests
    {
        NeuralNetworkBuilder testBuilder;
        Random rand;
        int enumTypeNeuronMaxNumber;
        public NeuralNetworkBuilderTests()
        {
            testBuilder = new NeuralNetworkBuilder();
            rand = new Random();
            enumTypeNeuronMaxNumber = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>().Count();
        }

        [Fact]
        public void BuilderShouldBuildNeuranNetworkFine()
        {
            uint numberOfHiddenLayers = (uint)rand.Next(1, 50);
            testBuilder.AddInputLayer((uint)rand.Next(1, 50), (uint)rand.Next(1, 50), (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            testBuilder.AddHiddenLayers((uint)rand.Next(1, 50), numberOfHiddenLayers, (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            testBuilder.AddOutputLayers((uint)rand.Next(1, 50), (NeuronType)rand.Next(enumTypeNeuronMaxNumber));

            Neural_Network testNeuralNetwork = testBuilder.GetNeural_Network();
            testNeuralNetwork._layersOfNeurons.Count.Should().Be((int)numberOfHiddenLayers + 2);
        }
        [Fact]
        public void BuilderCaseTwoShouldBuildNeuranNetworkFine()
        {
            uint numberOfHiddenLayers = (uint)rand.Next(1, 50);
            testBuilder.AddInputLayer((uint)rand.Next(1, 50), (uint)rand.Next(1, 50), (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            List<uint> numberOfNeuronesByHiddenLayerList = new List<uint>();
            for (int i = 0; i < numberOfHiddenLayers; i++)
                numberOfNeuronesByHiddenLayerList.Add((uint)rand.Next(1, 50));
            testBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayerList, numberOfHiddenLayers, (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            testBuilder.AddOutputLayers((uint)rand.Next(1, 50), (NeuronType)rand.Next(enumTypeNeuronMaxNumber));

            Neural_Network testNeuralNetwork = testBuilder.GetNeural_Network();
            testNeuralNetwork._layersOfNeurons.Count.Should().Be((int)numberOfHiddenLayers + 2);
        }


        [Fact]
        public void BuilderShouldThrowYouNeedToCreateInputLayerFirstException()
        {
            uint numberOfHiddenLayers = (uint)rand.Next(1, 50);
            Action AddHiddenLayers = () =>
            {
                testBuilder.AddHiddenLayers((uint)rand.Next(1, 50), numberOfHiddenLayers, (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            };
            AddHiddenLayers.Should().Throw<YouNeedToCreateInputLayerFirstException>();
        }
        [Fact]
        public void BuilderShouldThrowYouNeedToCreateInputLayerFirstExceptionCaseTwo()
        {
            Action AddHiddenLayers = () =>
            {
                testBuilder.AddOutputLayers((uint)rand.Next(1, 50), (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            };
            AddHiddenLayers.Should().Throw<YouNeedToCreateInputLayerFirstException>();
        }

        [Fact]
        public void BuilderShouldThrowArgumentException()
        {
            uint numberOfHiddenLayers =(uint) rand.Next(1, 50);
            testBuilder.AddInputLayer((uint)rand.Next(1, 50), (uint)rand.Next(1, 50), (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            List<uint> numberOfNeuronesByHiddenLayer = new List<uint>();
            for (int i = 0; i < numberOfHiddenLayers-3; i++)
                numberOfNeuronesByHiddenLayer.Add((uint)rand.Next(1, 50));
            Action AddHiddenLayers = () =>
            {
                testBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayer, numberOfHiddenLayers, (NeuronType)rand.Next(enumTypeNeuronMaxNumber));
            };
            AddHiddenLayers.Should().Throw<ArgumentException>();
        }

    }
}