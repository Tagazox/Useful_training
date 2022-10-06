using System.Diagnostics;
using Moq;
using Useful_training.Core.NeuralNetwork.Factory;
using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;
using Useful_training.Core.NeuralNetwork.Trainers;
using Useful_training.Core.NeuralNetwork.Trainers.Adapter;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests
{
    public class NeuralNetworkTrainerTests
    {
        private readonly Mock<INeuralNetworkTrainerContainer> _neuralNetworkContainer;
        private readonly List<DataSet> _dataSets;
        private readonly uint _numberOfInput;
        private readonly uint _numberOfOutput;
        private NeuralNetworkTrainer _neuralNetworkTrainer;

        public NeuralNetworkTrainerTests()
        {
            Random rand = new Random();
            _numberOfInput = 2;
            _numberOfOutput = 1;
            int numberOfDataset = 100;
            _neuralNetworkContainer = new Mock<INeuralNetworkTrainerContainer>();

            _dataSets = new List<DataSet>();
            for (int i = 0; i < numberOfDataset; i++)
            {
                double input1, input2, output;
                input1 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;
                input2 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;

                output = (input1 == 1 && input2 == 1) ? 1 : 0;
                List<double> inputs = new List<double>() { input1, input2 };
                List<double>? Outputs = new List<double>() { output };
                _dataSets.Add(new DataSet(inputs, Outputs));
            }
        }
        private void CreateNewNeuralNetwork()
        {
            uint numberOfInputNeurons = _numberOfInput;
            uint numberOfNeuronesByHiddenLayer = 5;
            uint numberOfHiddenLayers = 3;
            NeuralNetworkBuilder mockedNeuralNetworkBuilder = new NeuralNetworkBuilder();
            mockedNeuralNetworkBuilder.Initialize(numberOfInputNeurons, .005, .025);
            mockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayer, numberOfHiddenLayers, NeuronType.Tanh);
            mockedNeuralNetworkBuilder.AddOutputLayers(_numberOfOutput, NeuronType.Tanh);
            _neuralNetworkContainer.Setup(c => c.NeuralNetwork).Returns(mockedNeuralNetworkBuilder.GetNeuralNetwork());
        }
        [Fact]
        public void NeuralNetworkTrainerShouldTrainNeuralNetworkGood()
        {
            Debug.WriteLine("Warning: this test is currently disabled");
            /*
            _neuralNetworkContainer.Setup(c => c.DataSets).Returns(_dataSets);
            CreateNewNeuralNetwork();
            _neuralNetworkTrainer = new NeuralNetworkTrainer(_neuralNetworkContainer.Object,1);
            _neuralNetworkTrainer.TrainNeuralNetwork();

            INeuralNetwork trainedNeuralNetwork = _neuralNetworkContainer.Object.NeuralNetwork;

            foreach (DataSet set in _dataSets.Take(10))
            {
                IList<double> resultOutputs = trainedNeuralNetwork.Calculate(set.Inputs);
                for (int i = 0; i < set.TargetOutput.Count; i++)
                {
                    resultOutputs[i].Should().BeApproximately(set.TargetOutput[i], 0.1);
                }
            }*/
        }
        [Fact]
        public void CreateNeuralNetworkTrainerShouldThrowArgumentExceptionCase1()
        {
            CreateNewNeuralNetwork();
            Action TrainNeuralNetwork = () =>
            {
                _neuralNetworkTrainer = new NeuralNetworkTrainer(_neuralNetworkContainer.Object);
            };

            TrainNeuralNetwork.Should().Throw<NullReferenceException>();
        }
        [Fact]
        public void CreateNeuralNetworkTrainerShouldThrowArgumentExceptionCase2()
        {
            INeuralNetwork TrainedNeuralNetwork = null;
            _neuralNetworkContainer.Setup(c => c.NeuralNetwork).Returns(TrainedNeuralNetwork);
            _neuralNetworkContainer.Setup(c => c.DataSets).Returns(_dataSets);

            Action TrainNeuralNetwork = () =>
            {
                _neuralNetworkTrainer = new NeuralNetworkTrainer(_neuralNetworkContainer.Object);
            };

            TrainNeuralNetwork.Should().Throw<NullReferenceException>();
        }
    }
}