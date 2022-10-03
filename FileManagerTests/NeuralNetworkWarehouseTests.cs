using FileManager;
using Moq;
using FluentAssertions;
using Useful_training.Core.Neural_network;
using Useful_training.Core.Neural_network.Interface;
using FileManager.Exceptions;

namespace FileManagerTests
{
    public class NeuralNetworkWarehouseTests
    {
        NeuralNetworkWarehouse TestSubject;
        Mock<INeuralNetworkTrainerContainer> neural_NetworkContainer;
        List<double> input;
        public NeuralNetworkWarehouseTests()
        {
            neural_NetworkContainer = new Mock<INeuralNetworkTrainerContainer>();
            TestSubject = new NeuralNetworkWarehouse();
            CreateNewNeuralNetwork();
        }
        private void CreateNewNeuralNetwork()
        {
            input = new List<double> { 1, 0 };
            uint numberOfInput = 2;
            uint numberOfOutput = 2;
            uint numberOfInputNeurons = numberOfInput;
            uint numberOfNeuronesByHiddenLayerOutput = 5;
            uint numberOfHiddenLayers = 3;
            NeuralNetworkBuilder MockedNeuralNetworkBuilder = new NeuralNetworkBuilder();
            MockedNeuralNetworkBuilder.Initialize(numberOfInputNeurons, .005, 0.025);
            MockedNeuralNetworkBuilder.AddHiddenLayers(numberOfNeuronesByHiddenLayerOutput, numberOfHiddenLayers, NeuronType.Tanh);
            MockedNeuralNetworkBuilder.AddOutputLayers(numberOfOutput, NeuronType.Tanh);
            neural_NetworkContainer.Setup(c => c.Neural_Network).Returns(MockedNeuralNetworkBuilder.GetNeural_Network());
        }

        [Fact]
        public void SaveShouldBeGood()
        {
            string nameOfTheNeuralNetwork = Guid.NewGuid().ToString();

            Action Save = () =>
            {
                TestSubject.Save(neural_NetworkContainer.Object.Neural_Network, nameOfTheNeuralNetwork);
            };
            Save.Should().NotThrow();
        }

        [Fact]
        public void RetreiveShouldBeGood()
        {
            string nameOfTheNeuralNetwork = Guid.NewGuid().ToString();

            TestSubject.Save(neural_NetworkContainer.Object.Neural_Network, nameOfTheNeuralNetwork).Wait();
            INeuralNetwork neuralNetRecovred = TestSubject.Retreive<NeuralNetwork>(nameOfTheNeuralNetwork);
            neuralNetRecovred.Calculate(input).Should().BeEquivalentTo(neural_NetworkContainer.Object.Neural_Network.Calculate(input));
        }
        [Fact]
        public void OverrideShouldBeGood()
        {
            string nameOfTheNeuralNetwork = Guid.NewGuid().ToString();
            TestSubject.Save(neural_NetworkContainer.Object.Neural_Network, nameOfTheNeuralNetwork).Wait();
            INeuralNetwork oldSavedNeuralNetwork = neural_NetworkContainer.Object.Neural_Network;
           
            CreateNewNeuralNetwork();
            TestSubject.Override(neural_NetworkContainer.Object.Neural_Network, nameOfTheNeuralNetwork).Wait();
            
            INeuralNetwork neuralNetRecovred = TestSubject.Retreive<NeuralNetwork>(nameOfTheNeuralNetwork);
            
            oldSavedNeuralNetwork.Should().NotBeSameAs(neuralNetRecovred);
            neuralNetRecovred.Calculate(input).Should().BeEquivalentTo(neural_NetworkContainer.Object.Neural_Network.Calculate(input));
        }
        [Fact]
        public void SearchNeuralNetworkAvailableShouldBeGood()
        {
            string nameOfTheNeuralNetwork = Guid.NewGuid().ToString();
            string BaseName = nameOfTheNeuralNetwork;
            string searchTerm = "abc";

            for (int i = 0; i < 10; i++)
            {
                nameOfTheNeuralNetwork += searchTerm;
                TestSubject.Save(neural_NetworkContainer.Object.Neural_Network, nameOfTheNeuralNetwork);
            }
            for (int i = 0; i < 10; i++)
                TestSubject.SearchAvailable(BaseName, 0, i).Count().Should().Be(i);
        }

        [Fact]
        public void SaveShouldThrowAlreadyExistException()
        {
            string nameOfTheNeuralNetwork = Guid.NewGuid().ToString();
            TestSubject.Save(neural_NetworkContainer.Object.Neural_Network, nameOfTheNeuralNetwork).Wait();

            Action Save = () =>
            {
                TestSubject.Save(neural_NetworkContainer.Object.Neural_Network, nameOfTheNeuralNetwork).Wait();
            };
            Save.Should().Throw<AlreadyExistException>();
        }
        [Fact]
        public void RetreiveShouldThrowCantFindNeuralNetworkException()
        {
            string nameOfTheNeuralNetwork = Guid.NewGuid().ToString();
            Action Save = () =>
            {
                INeuralNetwork neuralNetRecovred = TestSubject.Retreive<NeuralNetwork>(nameOfTheNeuralNetwork);
            };
            Save.Should().Throw<CantFindException>();
        }
        [Fact]
        public void RetreiveShouldThrowException()
        {
            string nameOfTheNeuralNetwork = Guid.NewGuid().ToString();
            Action Save = () =>
            {
                INeuralNetwork neuralNetRecovred = TestSubject.Retreive<INeuralNetwork>(nameOfTheNeuralNetwork);
            };
            Save.Should().Throw<Exception>();
        }
    }
}