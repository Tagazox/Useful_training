using Useful_training.Core.NeuralNetwork.Exceptions;

namespace Useful_training.Core.NeuralNetwork.NeuralNetworkTests
{
    public class NeuralNetworkBuilderTest
    {
        NeuralNetworkBuilder TestBuilder;
        Random Random;
        public NeuralNetworkBuilderTest()
        {
            TestBuilder = new NeuralNetworkBuilder();
            Random = new Random();

        }
        [Fact]
        public void BuilderInitializeShouldBeGood()
        {
            TestBuilder.Initialize((uint)Random.Next(1, 10),Random.NextDouble(), Random.NextDouble());
        }
        [Fact]
        public void BuilderInitializeShouldThrowArgumentException()
        {
            Action InitializeBadLearnRate = () =>
            {
                TestBuilder.Initialize(2,-.5);
            };
            Action InitializeBadMomentum = () =>
            {
                TestBuilder.Initialize(2, .5,-.5);
            };
            InitializeBadLearnRate.Should().Throw<ArgumentException>();
            InitializeBadMomentum.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void BuilderAddHiddenLayerShouldBeGoodCase1()
        {
            TestBuilder.Initialize(2);
            uint numberOfHiddenLayer = (uint)Random.Next(1, 10);
            TestBuilder.AddHiddenLayers(1, numberOfHiddenLayer, NeuronType.Sigmoid);
            TestBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount((int)numberOfHiddenLayer);
        }
        [Fact]
        public void BuilderAddHiddenLayerShouldBeGoodCase2()
        {
            TestBuilder.Initialize(2);
            uint numberOfHiddenLayer = (uint)Random.Next(1, 10);
            List<uint> numberOfNeuronesBylayers = new List<uint>();
            for (int i = 0; i < numberOfHiddenLayer; i++)
            {
                numberOfNeuronesBylayers.Add((uint)Random.Next(1, 10));
            }
            TestBuilder.AddHiddenLayers(numberOfNeuronesBylayers, numberOfHiddenLayer, NeuronType.Sigmoid);
            TestBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount((int)numberOfHiddenLayer);
        }
        [Fact]
        public void BuilderAddHiddenLayerShouldThrowArgumentException()
        {
            uint numberOfHiddenLayer = (uint)Random.Next(1, 10);
            List<uint> numberOfNeuronesBylayers = new List<uint>();
            for (int i = 0; i < numberOfHiddenLayer - 1; i++)
            {
                numberOfNeuronesBylayers.Add((uint)Random.Next(1, 10));
            }
            Action AddHiddenLayer = () =>
            {
                TestBuilder.AddHiddenLayers(numberOfNeuronesBylayers, numberOfHiddenLayer, NeuronType.Sigmoid);
            };
            AddHiddenLayer.Should().Throw<ArgumentException>();
        }
        [Fact]
        public void BuilderAddOutputLayerShouldBeGood()
        {
            TestBuilder.Initialize(2);
            uint numberOfoutputs = (uint)Random.Next(1, 10);
            TestBuilder.AddOutputLayers(numberOfoutputs, NeuronType.Sigmoid);
            TestBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount(1);
        }
        [Fact]
        public void BuilderAddOutputLayerShouldThrowArgumentException()
        {
            Action AddOutputLayers = () =>
            {
                TestBuilder.AddOutputLayers(0, NeuronType.Sigmoid);
            };
            AddOutputLayers.Should().Throw<ArgumentException>();
        }
    }
}