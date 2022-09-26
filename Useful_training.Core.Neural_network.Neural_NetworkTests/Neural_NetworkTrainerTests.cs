using Moq;
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class Neural_NetworkTrainerTests
    {
        Mock<INeuralNetworkContainer> _neural_NetworkContainer;
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
            _neural_NetworkContainer = new Mock<INeuralNetworkContainer>();
            NeuralNetworkBuilder MockedNeuralNetworkBuilder = new NeuralNetworkBuilder(numberOfInputNeurons, .005, 0.025);
            MockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayerOutput, numberOfHiddenLayers, NeuronType.Tanh);
            MockedNeuralNetworkBuilder.AddOutputLayers(numberOfOutput, NeuronType.Tanh);
            _neural_NetworkContainer.Setup(c => c.GetNeuralNetwork()).Returns(MockedNeuralNetworkBuilder.GetNeural_Network());
        }
        [Fact]
        public void Neural_NetworkShouldBeTrainedGood()
        {
            Neural_Network TrainedNeuralNetwork = null;
            do
            {
                CreateNewNeuralNetwork();
                _neuralNetworkTrainer = new NeuralNetworkTrainer(_neural_NetworkContainer.Object);
                TrainedNeuralNetwork = _neuralNetworkTrainer.GetTrainedNeuralNetwork(dataSets);
            } while (TrainedNeuralNetwork == null);

            
            foreach (DataSet set in dataSets.Take(10))
            {
                IList<double> ResultOutputs = TrainedNeuralNetwork.Calculate(set.Values);
                for (int i = 0; i < set.Targets.Count; i++)
                {
                    ResultOutputs[i].Should().BeApproximately(set.Targets[i], 0.01);
                }
            }


        }

    }
}