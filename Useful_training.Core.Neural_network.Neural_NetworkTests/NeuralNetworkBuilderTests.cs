using Useful_training.Core.NeuralNetwork.Exceptions;

namespace Useful_training.Core.NeuralNetwork.NeuralNetworkTests
{
    public class NeuralNetworkBuilderTest
    {
        NeuralNetworkBuilder testBuilder;
        Random rand;
        public NeuralNetworkBuilderTest()
        {
            testBuilder = new NeuralNetworkBuilder();
            rand = new Random();

        }
        [Fact]
        public void BuilderShouldInitializeGood()
        {
            testBuilder.Initialize((uint)rand.Next(1, 10),rand.NextDouble(), rand.NextDouble());
        }
        [Fact]
        public void BuilderShouldInitializeThrowArgumentException()
        {
            Action InitializeBadLearnRate = () =>
            {
                testBuilder.Initialize(2,-.5);
            };
            Action InitializeBadMomentum = () =>
            {
                testBuilder.Initialize(2, .5,-.5);
            };
            InitializeBadLearnRate.Should().Throw<ArgumentException>();
            InitializeBadMomentum.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void BuilderShouldAddHiddenLayerGoodCase1()
        {
            testBuilder.Initialize(2);
            uint numberOfHiddenLayer = (uint)rand.Next(1, 10);
            testBuilder.AddHiddenLayers(1, numberOfHiddenLayer, NeuronType.Sigmoid);
            testBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount((int)numberOfHiddenLayer);
        }
        [Fact]
        public void BuilderShouldAddHiddenLayerGoodCase2()
        {
            testBuilder.Initialize(2);
            uint numberOfHiddenLayer = (uint)rand.Next(1, 10);
            List<uint> numberOfNeuronesBylayers = new List<uint>();
            for (int i = 0; i < numberOfHiddenLayer; i++)
            {
                numberOfNeuronesBylayers.Add((uint)rand.Next(1, 10));
            }
            testBuilder.AddHiddenLayers(numberOfNeuronesBylayers, numberOfHiddenLayer, NeuronType.Sigmoid);
            testBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount((int)numberOfHiddenLayer);
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
            testBuilder.Initialize(2);
            uint numberOfoutputs = (uint)rand.Next(1, 10);
            testBuilder.AddOutputLayers(numberOfoutputs, NeuronType.Sigmoid);
            testBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount(1);
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