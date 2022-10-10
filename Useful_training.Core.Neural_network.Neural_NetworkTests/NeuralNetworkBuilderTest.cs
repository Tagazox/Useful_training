using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests;

public class NeuralNetworkBuilderTest
{
    private readonly NeuralNetworkBuilder _testBuilder;
    private readonly Random _random;
    public NeuralNetworkBuilderTest()
    {
        _testBuilder = new NeuralNetworkBuilder();
        _random = new Random();

    }
    [Fact]
    public void BuilderInitializeShouldBeGood()
    {
        _testBuilder.Initialize((uint)_random.Next(1, 10),_random.NextDouble(), _random.NextDouble());
    }
    [Fact]
    public void BuilderInitializeShouldThrowArgumentException()
    {
        Action initializeBadLearnRate = () =>
        {
            _testBuilder.Initialize(2,-.5);
        };
        Action initializeBadMomentum = () =>
        {
            _testBuilder.Initialize(2, .5,-.5);
        };
        initializeBadLearnRate.Should().Throw<ArgumentException>();
        initializeBadMomentum.Should().Throw<ArgumentException>();
    }
    [Fact]
    public void BuilderAddHiddenLayerShouldBeGoodCase1()
    {
        _testBuilder.Initialize(2);
        uint numberOfHiddenLayer = (uint)_random.Next(1, 10);
        _testBuilder.AddHiddenLayers(1, numberOfHiddenLayer, NeuronType.Sigmoid);
        _testBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount((int)numberOfHiddenLayer);
    }
    [Fact]
    public void BuilderAddHiddenLayerShouldBeGoodCase2()
    {
        _testBuilder.Initialize(2);
        uint numberOfHiddenLayer = (uint)_random.Next(1, 10);
        List<uint> numberOfNeuronesByLayers = new List<uint>();
        for (int i = 0; i < numberOfHiddenLayer; i++)
        {
            numberOfNeuronesByLayers.Add((uint)_random.Next(1, 10));
        }
        _testBuilder.AddHiddenLayers(numberOfNeuronesByLayers, numberOfHiddenLayer, NeuronType.Sigmoid);
        _testBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount((int)numberOfHiddenLayer);
    }
    [Fact]
    public void BuilderAddHiddenLayerShouldThrowArgumentException()
    {
        uint numberOfHiddenLayer = (uint)_random.Next(1, 10);
        List<uint> numberOfNeuronesLayers = new List<uint>();
        for (int i = 0; i < numberOfHiddenLayer - 1; i++)
        {
            numberOfNeuronesLayers.Add((uint)_random.Next(1, 10));
        }
        Action addHiddenLayer = () =>
        {
            _testBuilder.AddHiddenLayers(numberOfNeuronesLayers, numberOfHiddenLayer, NeuronType.Sigmoid);
        };
        addHiddenLayer.Should().Throw<ArgumentException>();
    }
    [Fact]
    public void BuilderAddOutputLayerShouldBeGood()
    {
        _testBuilder.Initialize(2);
        uint numberOutputs = (uint)_random.Next(1, 10);
        _testBuilder.AddOutputLayers(numberOutputs, NeuronType.Sigmoid);
        _testBuilder.GetNeuralNetwork().LayersOfNeurons.Should().HaveCount(1);
    }
    [Fact]
    public void BuilderAddOutputLayerShouldThrowArgumentException()
    {
        Action addOutputLayers = () =>
        {
            _testBuilder.AddOutputLayers(0, NeuronType.Sigmoid);
        };
        addOutputLayers.Should().Throw<ArgumentException>();
    }
}