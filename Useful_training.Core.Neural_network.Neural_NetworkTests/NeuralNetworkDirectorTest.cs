using Moq;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Interfaces;

namespace Useful_training.Core.NeuralNetwork.NeuralNetworkTests
{
    public class NeuralNetworkDirectorTest
    {
        NeuralNetworkDirector testDirector;
        Mock<INeuralNetworkBuilder> networkBuilderMock;
        public NeuralNetworkDirectorTest()
        {
            testDirector = new NeuralNetworkDirector();
            networkBuilderMock = new Mock<INeuralNetworkBuilder>();
        }
        [Fact]
        public void BuilderBuildMinimalNeuralNetworkShouldBeGood()
        {
            testDirector.networkBuilder = networkBuilderMock.Object;
            Action BuildMinimalNeuralNetwork = () =>
            {
                testDirector.BuildMinimalNeuralNetwork(2, 2, NeuronType.Sigmoid);
            };
            BuildMinimalNeuralNetwork.Should().NotThrow<Exception>();
        }
        [Fact]
        public void BuilderBuildComplexeNeuralNetworkShouldBeGoodCase1()
        {
            testDirector.networkBuilder = networkBuilderMock.Object;
            Action BuildComplexeNeuralNetwork = () =>
            {
                testDirector.BuildComplexeNeuralNetwork(2, .5, .5, 2, 2, 2, NeuronType.Sigmoid);
            };
            BuildComplexeNeuralNetwork.Should().NotThrow<Exception>();
        }
        [Fact]
        public void BuilderBuildComplexeNeuralNetworkShouldBeGoodCase2()
        {
            Random rand = new Random();
            testDirector.networkBuilder = networkBuilderMock.Object;
            List<uint> numbersOfNeuronesByHiddenLayer = new List<uint>();
            uint numberOfHiddenLayer = 5;
            for (int i = 0; i < numberOfHiddenLayer; i++)
            {
                numbersOfNeuronesByHiddenLayer.Add((uint)rand.Next(1, 10));
            }
            Action BuildComplexeNeuralNetwork = () =>
            {
                testDirector.BuildComplexeNeuralNetwork(2, .5, .5, 2, numberOfHiddenLayer, numbersOfNeuronesByHiddenLayer, NeuronType.Sigmoid);
            };
            BuildComplexeNeuralNetwork.Should().NotThrow<Exception>();
        }

        [Fact]
        public void BuilderBuildMinimalNeuralNetworkShouldThrowBuilderNotDefinedException()
        {
            Action BuildMinimalNeuralNetwork = () =>
            {
                testDirector.BuildMinimalNeuralNetwork(2, 2, NeuronType.Sigmoid);
            };
            BuildMinimalNeuralNetwork.Should().Throw<BuilderNotDefinedException>();
        }
        [Fact]
        public void BuilderBuildComplexeNeuralNetworkShouldThrowBuilderNotDefinedExceptionCase1()
        {
            Action BuildComplexeNeuralNetwork = () =>
            {
                testDirector.BuildComplexeNeuralNetwork(2, .5, .5, 2, 2, 2, NeuronType.Sigmoid);
            };
            BuildComplexeNeuralNetwork.Should().Throw<BuilderNotDefinedException>();
        }
        [Fact]
        public void BuilderBuildComplexeNeuralNetworkShouldThrowBuilderNotDefinedExceptionCase2()
        {
            Random rand = new Random();
            List<uint> numbersOfNeuronesByHiddenLayer = new List<uint>();
            uint numberOfHiddenLayer = 5;
            for (int i = 0; i < numberOfHiddenLayer; i++)
            {
                numbersOfNeuronesByHiddenLayer.Add((uint)rand.Next(1, 10));
            }
            Action BuildComplexeNeuralNetwork = () =>
            {
                testDirector.BuildComplexeNeuralNetwork(2, .5, .5, 2, numberOfHiddenLayer, numbersOfNeuronesByHiddenLayer, NeuronType.Sigmoid);
            };
            BuildComplexeNeuralNetwork.Should().Throw<BuilderNotDefinedException>();
        }
    }
}