using Moq;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.Factory.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests;

public class NeuralNetworkDirectorTest
{
    private readonly NeuralNetworkDirector _testDirector;
    private readonly Mock<INeuralNetworkBuilder> _networkBuilderMock;
    public NeuralNetworkDirectorTest()
    {
        _testDirector = new NeuralNetworkDirector();
        _networkBuilderMock = new Mock<INeuralNetworkBuilder>();
    }
    [Fact]
    public void BuilderBuildMinimalNeuralNetworkShouldBeGood()
    {
        _testDirector.NetworkBuilder = _networkBuilderMock.Object;
        Action buildMinimalNeuralNetwork = () =>
        {
            _testDirector.BuildMinimalNeuralNetwork(2, 2, NeuronType.Sigmoid);
        };
        buildMinimalNeuralNetwork.Should().NotThrow<Exception>();
    }
    [Fact]
    public void BuilderBuildComplexNeuralNetworkShouldBeGoodCase1()
    {
        _testDirector.NetworkBuilder = _networkBuilderMock.Object;
        Action buildComplexNeuralNetwork = () =>
        {
            _testDirector.BuildComplexNeuralNetwork(2, .5, .5, 2, 2, 2, NeuronType.Sigmoid);
        };
        buildComplexNeuralNetwork.Should().NotThrow<Exception>();
    }
    [Fact]
    public void BuilderBuildComplexNeuralNetworkShouldBeGoodCase2()
    {
        Random rand = new Random();
        _testDirector.NetworkBuilder = _networkBuilderMock.Object;
        List<uint> numbersOfNeuronesByHiddenLayer = new List<uint>();
        const uint numberOfHiddenLayer = 5;
        for (int i = 0; i < numberOfHiddenLayer; i++)
        {
            numbersOfNeuronesByHiddenLayer.Add((uint)rand.Next(1, 10));
        }
        Action buildComplexNeuralNetwork = () =>
        {
            _testDirector.BuildComplexNeuralNetwork(2, .5, .5, 2, numberOfHiddenLayer, numbersOfNeuronesByHiddenLayer, NeuronType.Sigmoid);
        };
        buildComplexNeuralNetwork.Should().NotThrow<Exception>();
    }

    [Fact]
    public void BuilderBuildMinimalNeuralNetworkShouldThrowBuilderNotDefinedException()
    {
        Action buildMinimalNeuralNetwork = () =>
        {
            _testDirector.BuildMinimalNeuralNetwork(2, 2, NeuronType.Sigmoid);
        };
        buildMinimalNeuralNetwork.Should().Throw<BuilderNotDefinedException>();
    }
    [Fact]
    public void BuilderBuildComplexNeuralNetworkShouldThrowBuilderNotDefinedExceptionCase1()
    {
        Action buildComplexNeuralNetwork = () =>
        {
            _testDirector.BuildComplexNeuralNetwork(2, .5, .5, 2, 2, 2, NeuronType.Sigmoid);
        };
        buildComplexNeuralNetwork.Should().Throw<BuilderNotDefinedException>();
    }
    [Fact]
    public void BuilderBuildComplexNeuralNetworkShouldThrowBuilderNotDefinedExceptionCase2()
    {
        Random rand = new Random();
        List<uint> numbersOfNeuronesByHiddenLayer = new List<uint>();
        const uint numberOfHiddenLayer = 5;
        for (int i = 0; i < numberOfHiddenLayer; i++)
        {
            numbersOfNeuronesByHiddenLayer.Add((uint)rand.Next(1, 10));
        }
        Action buildComplexNeuralNetwork = () =>
        {
            _testDirector.BuildComplexNeuralNetwork(2, .5, .5, 2, numberOfHiddenLayer, numbersOfNeuronesByHiddenLayer, NeuronType.Sigmoid);
        };
        buildComplexNeuralNetwork.Should().Throw<BuilderNotDefinedException>();
    }
}