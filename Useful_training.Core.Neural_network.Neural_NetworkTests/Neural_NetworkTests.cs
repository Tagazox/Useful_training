using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class Neural_NetworkTests
    {
        Neural_Network _neural_Network;
        IEnumerable<NeuronType> NeuronTypeAvailable;
        List<double> input;
        uint numberOfInput;
        uint numberOfNeurons;

        public Neural_NetworkTests()
        {
            _neural_Network = new Neural_Network();
            NeuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
            numberOfInput = 2;
            numberOfNeurons = 3;
            input = new List<double> { 1, 2 };
        }
        [Fact]
        public void Neural_NetworkShouldAddLayerGood()
        {
            foreach (NeuronType type in NeuronTypeAvailable)
                _neural_Network.AddLayer(1, 1, type);
            _neural_Network._layersOfNeurons.Count.Should().Be(NeuronTypeAvailable.Count());
        }
        [Fact]
        public void Neural_NetworkShouldCalculateGood()
        {
            _neural_Network.AddLayer(numberOfInput, numberOfNeurons, NeuronType.Binary);
            for (int i = 0; i < 5; i++)
            {
                _neural_Network.AddLayer(numberOfNeurons, numberOfNeurons, NeuronType.Binary);
            }
            _neural_Network.Calculate(input).Count.Should().Be((int)numberOfNeurons);
        }
        [Fact]
        public void Neural_NetworkCalculateShouldThrowNeedToBeCreatedByTheBuilderException()
        {
            Action Calculate = () =>
            {
                _neural_Network.Calculate(input);
            };
            Calculate.Should().Throw<NeedToBeCreatedByTheBuilderException>();
        }
        [Fact]
        public void Neural_NetworkCalculateShouldThrowWrongInputForCalculationException()
        {
            List<double> inputForThisTest = new List<double> { 1 };
            _neural_Network.AddLayer(numberOfInput, numberOfNeurons, NeuronType.Binary);
            Action Calculate = () =>
            {
                _neural_Network.Calculate(inputForThisTest);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
        [Fact]
        public void Neural_NetworkAddLayerShouldThrowCantInitializeWithZeroInputException()
        {
            Action AddLayer = () =>
            {
                _neural_Network.AddLayer(0, numberOfNeurons, NeuronType.Binary);
            };
            AddLayer.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void Neural_NetworkAddLayerShouldThrowCantInitializeWithZeroNeuronException()
        {
            Action AddLayer = () =>
            {
                _neural_Network.AddLayer(numberOfInput, 0, NeuronType.Binary);
            };
            AddLayer.Should().Throw<CantInitializeWithZeroNeuronException>();
        }

    }
}