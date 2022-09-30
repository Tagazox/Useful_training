using Moq;
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.ValueObject;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class Neural_NetworkTrainerTests
    {
        Mock<INeuralNetworkTrainerContainer> _neural_NetworkContainer;
        List<DataSet> dataSets;
        uint numberOfInput;
        uint numberOfOutput;
        Random rand;
        NeuralNetworkTrainer _neuralNetworkTrainer;

        public Neural_NetworkTrainerTests()
        {
            rand = new Random();
            numberOfInput = 2;
            numberOfOutput = 1;
            int numberOfDataset = 100;
            _neural_NetworkContainer = new Mock<INeuralNetworkTrainerContainer>();

            dataSets = new List<DataSet>();
            for (int i = 0; i < numberOfDataset; i++)
            {
                double input1, input2, input3, input4, output;
                input1 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;
                input2 = (rand.NextDouble() * 2 - 1) > 0 ? 1 : 0;

                output = (input1 == 1 && input2 == 1) ? 1 : 0;
                List<double> inputs = new List<double>() { input1, input2 };
                List<double> Outputs = new List<double>() { output };
                dataSets.Add(new DataSet(inputs, Outputs));
            }
        }
        private void CreateNewNeuralNetwork()
        {
            uint numberOfInputNeurons = numberOfInput;
            uint numberOfNeuronesByHiddenLayerOutput = 5;
            uint numberOfHiddenLayers = 3;
            NeuralNetworkBuilder MockedNeuralNetworkBuilder = new NeuralNetworkBuilder(numberOfInputNeurons, .005, 0.025);
            MockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayerOutput, numberOfHiddenLayers, NeuronType.Tanh);
            MockedNeuralNetworkBuilder.AddOutputLayers(numberOfOutput, NeuronType.Tanh);
            _neural_NetworkContainer.Setup(c => c.Neural_Network).Returns(MockedNeuralNetworkBuilder.GetNeural_Network());
        }
        [Fact]
        public void Neural_NetworkShouldBeTrainedGood()
        {
            _neural_NetworkContainer.Setup(c => c.DataSets).Returns(dataSets);
            CreateNewNeuralNetwork();
            _neuralNetworkTrainer = new NeuralNetworkTrainer(_neural_NetworkContainer.Object);
            _neuralNetworkTrainer.TrainNeuralNetwork();

            INeural_Network TrainedNeuralNetwork = _neural_NetworkContainer.Object.Neural_Network;

            foreach (DataSet set in dataSets.Take(10))
            {
                IList<double> ResultOutputs = TrainedNeuralNetwork.Calculate(set.Values);
                for (int i = 0; i < set.Targets.Count; i++)
                {
                    ResultOutputs[i].Should().BeApproximately(set.Targets[i], 0.01);
                }
            }
        }
        [Fact]
        public void Neural_NetworkGetTrainedNeuronShouldThrowArgumentExceptionCase1()
        {
            INeural_Network TrainedNeuralNetwork = null;
            CreateNewNeuralNetwork();
            _neuralNetworkTrainer = new NeuralNetworkTrainer(_neural_NetworkContainer.Object);

            Action TrainNeuralNetwork = () =>
            {
                _neuralNetworkTrainer.TrainNeuralNetwork();
            };

            TrainNeuralNetwork.Should().Throw<NullReferenceException>();
        }
        [Fact]
        public void Neural_NetworkGetTrainedNeuronShouldThrowArgumentExceptionCase2()
        {
            INeural_Network TrainedNeuralNetwork = null;
            _neural_NetworkContainer.Setup(c => c.DataSets).Returns(dataSets);
            _neuralNetworkTrainer = new NeuralNetworkTrainer(_neural_NetworkContainer.Object);

            Action TrainNeuralNetwork = () =>
            {
                _neuralNetworkTrainer.TrainNeuralNetwork();
            };

            TrainNeuralNetwork.Should().Throw<NullReferenceException>();
        }
    }
}