using Moq;
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class Neural_NetworkTrainerTests
    {
        Mock<INeuralNetworkContainer> _neural_NetworkContainer;
        List<double> input;
        List<double> ExceptedOutputs;
        uint numberOfInput;
        uint numberOfOutput;
        Random rand;
        NeuralNetworkTrainer _neuralNetworkTrainer; 

        public Neural_NetworkTrainerTests()
        {
            rand = new Random();
            numberOfInput = 2;// (uint) rand.Next(2,5);
            numberOfOutput = 2;// (uint)rand.Next(2, 5);
            uint numberOfNeuronesByHiddenLayerOutput = (uint)rand.Next((int)numberOfInput, (int)numberOfInput +2);
            uint numberOfInputNeurons = (uint)rand.Next(2, 5);
            uint numberOfHiddenLayers = (uint)rand.Next(2, 25);
            int enumTypeNeuronMaxNumber = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>().Count();


            _neural_NetworkContainer = new Mock<INeuralNetworkContainer>();
            NeuralNetworkBuilder MockedNeuralNetworkBuilder = new NeuralNetworkBuilder();
            MockedNeuralNetworkBuilder.AddInputLayer(numberOfInput, (uint)rand.Next(2, 5), NeuronType.Elu);
           // MockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayerOutput, numberOfHiddenLayers, NeuronType.Elu);
            MockedNeuralNetworkBuilder.AddOutputLayers(numberOfOutput, NeuronType.Elu);
            _neural_NetworkContainer.Setup(c => c.GetNeuralNetwork()).Returns(MockedNeuralNetworkBuilder.GetNeural_Network());
            
            input = new List<double>();
            ExceptedOutputs = new List<double>();
            for (int i = 0; i < numberOfInput; i++)
                input.Add((rand.NextDouble() *2) -1);
            for (int i = 0; i < numberOfOutput; i++)
                ExceptedOutputs.Add((rand.NextDouble() *2) -1);
            _neuralNetworkTrainer = new NeuralNetworkTrainer(_neural_NetworkContainer.Object);
        }
      
        [Fact]
        public void Neural_NetworkShouldBeTrainedGood()
        {
            Neural_Network TrainedNeuralNetwork = _neuralNetworkTrainer.GetTrainedNeuralNetwork(input, ExceptedOutputs);
            IList<double> ResultOutputs = TrainedNeuralNetwork.Calculate(input);
            for (int i = 0; i < ExceptedOutputs.Count; i++)
            {
                ResultOutputs[i].Should().BeApproximately(ExceptedOutputs[i],0.01);
            }

        }

    }
}