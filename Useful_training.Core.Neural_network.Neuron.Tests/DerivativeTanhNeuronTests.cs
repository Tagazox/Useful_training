
using Useful_training.Core.Neural_network.Neuron.Exceptions;

namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class DerivativeTanhNeuronTests
    {
        Random _rand;
        int _numberOnInputs;
        List<double> _inputs;
        public DerivativeTanhNeuronTests()
        {
            _rand = new Random();
            _numberOnInputs = _rand.Next(1, 10);
            _inputs = new List<double>();
            for (int i = 0; i < _numberOnInputs; i++)
            {
                _inputs.Add(_rand.NextDouble() * 2 - 1);
            }
        }

        [Fact]
        public void NeuroneCalculationShouldBeOk()
        {
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            derivativeTanhNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = derivativeTanhNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(0, 1);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            derivativeTanhNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = derivativeTanhNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = derivativeTanhNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeTanhNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            Action Initialise = () =>
            {
                derivativeTanhNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            derivativeTanhNeuron.InitialiseWithRandomValues(_numberOnInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeTanhNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
