using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class NeuralNetworkBuilderTest
    {
        NeuralNetworkBuilder testBuilder;
        Random rand;
        public NeuralNetworkBuilderTest()
        {
            testBuilder = new NeuralNetworkBuilder(1);
            rand = new Random();

        }
        [Fact]
        public void BuilderShouldAddHiddenLayerGoodCase1()
        {
            uint numberOfHiddenLayer = (uint)rand.Next(1, 10);
            testBuilder.AddHiddenLayers(1, numberOfHiddenLayer, NeuronType.Sigmoid);
            testBuilder.GetNeural_Network().LayersOfNeurons.Should().HaveCount((int)numberOfHiddenLayer);
        }
        [Fact]
        public void BuilderShouldAddHiddenLayerGoodCase2()
        {
            uint numberOfHiddenLayer = (uint)rand.Next(1, 10);
            List<uint> numberOfNeuronesBylayers = new List<uint>();
            for (int i = 0; i < numberOfHiddenLayer; i++)
            {
                numberOfNeuronesBylayers.Add((uint)rand.Next(1, 10));
            }
            testBuilder.AddHiddenLayers(numberOfNeuronesBylayers, numberOfHiddenLayer, NeuronType.Sigmoid);
            testBuilder.GetNeural_Network().LayersOfNeurons.Should().HaveCount((int)numberOfHiddenLayer);
        }
        [Fact]
        public void BuilderAddHiddenLayerShouldThrowArgumentException()
        {
            uint numberOfHiddenLayer = (uint)rand.Next(1, 10);
            List<uint> numberOfNeuronesBylayers = new List<uint>();
            for (int i = 0; i < numberOfHiddenLayer - 1; i++)
            {
                numberOfNeuronesBylayers.Add((uint)rand.Next(1, 10));
            }
            Action AddHiddenLayer = () =>
            {
                testBuilder.AddHiddenLayers(numberOfNeuronesBylayers, numberOfHiddenLayer, NeuronType.Sigmoid);
            };
            AddHiddenLayer.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void BuilderShouldAddOutputLayerGood()
        {
            uint numberOfoutputs = (uint)rand.Next(1, 10);
            testBuilder.AddOutputLayers(numberOfoutputs, NeuronType.Sigmoid);
            testBuilder.GetNeural_Network().LayersOfNeurons.Should().HaveCount(1);
        }
        [Fact]
        public void BuilderAddOutputLayerShouldThrowArgumentException()
        {
            Action AddOutputLayers = () =>
            {
                testBuilder.AddOutputLayers(0, NeuronType.Sigmoid);
            };
            AddOutputLayers.Should().Throw<ArgumentException>();
        }
    }
}