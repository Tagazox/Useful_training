


namespace Useful_training.Core.Neural_network.Tests
{
    public class BinaryNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public BinaryNeuronTests()
        {
            _rand = new Random();
            _numberOfInputs =(uint) _rand.Next(1, 10);
            _inputs = new List<double>();
            for (int i = 0; i < _numberOfInputs; i++)
            {
                _inputs.Add(_rand.NextDouble() * 2 - 1);
            }
        }

        [Fact]
        public void NeuroneCalculationShouldBeOk()
        {
            BinaryNeuron binaryNeuron = new BinaryNeuron();
            binaryNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = binaryNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(0, 1);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            BinaryNeuron binaryNeuron = new BinaryNeuron();
            binaryNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = binaryNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = binaryNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            BinaryNeuron binaryNeuron = new BinaryNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = binaryNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneInitialisationShouldThrowCantInitializeWithZeroInputException()
        {
            BinaryNeuron binaryNeuron = new BinaryNeuron();
            Action Initialise = () =>
            {
                binaryNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            BinaryNeuron binaryNeuron = new BinaryNeuron();
            binaryNeuron.InitialiseWithRandomValues(_numberOfInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = binaryNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
