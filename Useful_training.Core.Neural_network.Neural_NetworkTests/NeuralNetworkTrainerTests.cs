using Moq;
using Useful_training.Core.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.ValueObject;

namespace Useful_training.Core.NeuralNetwork.NeuralNetworkTests
{
    public class NeuralNetworkTrainerTests
    {
        Mock<INeuralNetworkTrainerContainer> NeuralNetworkContainer;
        List<DataSet> dataSets;
        uint numberOfInput;
        uint numberOfOutput;
        Random rand;
        NeuralNetworkTrainer NeuralNetworkTrainer;

        public NeuralNetworkTrainerTests()
        {
            rand = new Random();
            numberOfInput = 2;
            numberOfOutput = 1;
            int numberOfDataset = 100;
            NeuralNetworkContainer = new Mock<INeuralNetworkTrainerContainer>();

            dataSets = new List<DataSet>();
            for (int i = 0; i < numberOfDataset; i++)
            {
                double input1, input2, output;
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
            uint numberOfNeuronesByHiddenLayer = 5;
            uint numberOfHiddenLayers = 3;
            NeuralNetworkBuilder MockedNeuralNetworkBuilder = new NeuralNetworkBuilder();
            MockedNeuralNetworkBuilder.Initialize(numberOfInputNeurons, .005, .025);
            MockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayer, numberOfHiddenLayers, NeuronType.Tanh);
            MockedNeuralNetworkBuilder.AddOutputLayers(numberOfOutput, NeuronType.Tanh);
            NeuralNetworkContainer.Setup(c => c.NeuralNetwork).Returns(MockedNeuralNetworkBuilder.GetNeuralNetwork());
        }
        [Fact]
        public void NeuralNetworkTrainerShouldTrainNeuralNetworkGood()
        {
            throw new NotImplementedException("This test is currently disabled");

            NeuralNetworkContainer.Setup(c => c.DataSets).Returns(dataSets);
            CreateNewNeuralNetwork();
            NeuralNetworkTrainer = new NeuralNetworkTrainer(NeuralNetworkContainer.Object,1);
            NeuralNetworkTrainer.TrainNeuralNetwork();

            INeuralNetwork TrainedNeuralNetwork = NeuralNetworkContainer.Object.NeuralNetwork;

            foreach (DataSet set in dataSets.Take(10))
            {
                IList<double> ResultOutputs = TrainedNeuralNetwork.Calculate(set.Inputs);
                for (int i = 0; i < set.TargetOutput.Count; i++)
                {
                    ResultOutputs[i].Should().BeApproximately(set.TargetOutput[i], 0.1);
                }
            }
        }
        [Fact]
        public void CreateNeuralNetworkTrainerShouldThrowArgumentExceptionCase1()
        {
            CreateNewNeuralNetwork();
            Action TrainNeuralNetwork = () =>
            {
                NeuralNetworkTrainer = new NeuralNetworkTrainer(NeuralNetworkContainer.Object);
            };

            TrainNeuralNetwork.Should().Throw<NullReferenceException>();
        }
        [Fact]
        public void CreateNeuralNetworkTrainerShouldThrowArgumentExceptionCase2()
        {
            INeuralNetwork TrainedNeuralNetwork = null;
            NeuralNetworkContainer.Setup(c => c.NeuralNetwork).Returns(TrainedNeuralNetwork);
            NeuralNetworkContainer.Setup(c => c.DataSets).Returns(dataSets);

            Action TrainNeuralNetwork = () =>
            {
                NeuralNetworkTrainer = new NeuralNetworkTrainer(NeuralNetworkContainer.Object);
            };

            TrainNeuralNetwork.Should().Throw<NullReferenceException>();
        }
    }
}