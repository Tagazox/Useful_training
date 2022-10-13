using System.Diagnostics;
using Moq;
using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;
using Useful_training.Core.NeuralNetwork.Trainers;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests;

public class NeuralNetworkTrainerTests
{
    private readonly Mock<INeuralNetworkTrainerContainer> _neuralNetworkContainer;
    private readonly List<DataSet> _dataSets;
    private readonly uint _numberOfInput;
    private readonly uint _numberOfOutput;

    public NeuralNetworkTrainerTests()
    {
        Random rand = new Random();
        _numberOfInput = 2;
        _numberOfOutput = 1;
        const int numberOfDataset = 100;
        _neuralNetworkContainer = new Mock<INeuralNetworkTrainerContainer>();

        _dataSets = new List<DataSet>();
        for (int i = 0; i < numberOfDataset; i++)
        {
            int input1 = rand.NextDouble() * 2 - 1 > 0 ? 1 : 0;
            int input2 = rand.NextDouble() * 2 - 1 > 0 ? 1 : 0;

            int output = input1 == 1 && input2 == 1 ? 1 : 0;
            List<double> inputs = new List<double> { input1, input2 };
            List<double> outputs = new List<double> { output };
            _dataSets.Add(new DataSet(inputs, outputs));
        }
    }
    private void CreateNewNeuralNetwork()
    {
        const uint numberOfNeuronesByHiddenLayer = 5;
        const uint numberOfHiddenLayers = 3;
        NeuralNetworkBuilder mockedNeuralNetworkBuilder = new NeuralNetworkBuilder();
        mockedNeuralNetworkBuilder.Initialize(_numberOfInput, .005, .025);
        mockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayer, numberOfHiddenLayers, NeuronType.Tanh);
        mockedNeuralNetworkBuilder.AddOutputLayers(_numberOfOutput, NeuronType.Tanh);
        _neuralNetworkContainer.Setup(c => c.NeuralNetwork).Returns(mockedNeuralNetworkBuilder.GetNeuralNetwork());
    }
    [Fact]
    public void NeuralNetworkTrainerShouldCancelGood()
    {
        _neuralNetworkContainer.Setup(c => c.DataSets).Returns(_dataSets);
        CreateNewNeuralNetwork();
        NeuralNetworkTrainer neuralNetworkTrainer = new NeuralNetworkTrainer(_neuralNetworkContainer.Object);
        Task.Run( () => {
            Thread.Sleep(5000);
            neuralNetworkTrainer.Destroy();
        } );
        neuralNetworkTrainer.TrainNeuralNetwork();

    }
    [Fact]
    public void CreateNeuralNetworkTrainerShouldThrowArgumentExceptionCase1()
    {
        CreateNewNeuralNetwork();
        Action trainNeuralNetwork = () =>
        {
            NeuralNetworkTrainer unused = new NeuralNetworkTrainer(_neuralNetworkContainer.Object);
        };

        trainNeuralNetwork.Should().Throw<NullReferenceException>();
    }
    [Fact]
    public void CreateNeuralNetworkTrainerShouldThrowArgumentExceptionCase2()
    {
        INeuralNetwork trainedNeuralNetwork = null!;
        _neuralNetworkContainer.Setup(c => c.NeuralNetwork).Returns(trainedNeuralNetwork);
        _neuralNetworkContainer.Setup(c => c.DataSets).Returns(_dataSets);

        Action trainNeuralNetwork = () =>
        {
            NeuralNetworkTrainer unused = new NeuralNetworkTrainer(_neuralNetworkContainer.Object);
        };

        trainNeuralNetwork.Should().Throw<NullReferenceException>();
    }
}