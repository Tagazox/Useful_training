
using Useful_training.Core.Neural_network.Neuron.Exceptions;

namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class DerivativeLeakyReLuNeuronTests
    {
        Random _rand;
        int _numberOnInputs;
        List<double> _inputs;
        public DerivativeLeakyReLuNeuronTests()
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
            DerivativeLeakyReLuNeuron derivativeLeakyReLuNeuron = new DerivativeLeakyReLuNeuron();
            derivativeLeakyReLuNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = derivativeLeakyReLuNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(0.01, 1);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            DerivativeLeakyReLuNeuron derivativeLeakyReLuNeuron = new DerivativeLeakyReLuNeuron();
            derivativeLeakyReLuNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = derivativeLeakyReLuNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = derivativeLeakyReLuNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            DerivativeLeakyReLuNeuron derivativeLeakyReLuNeuron = new DerivativeLeakyReLuNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeLeakyReLuNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            DerivativeLeakyReLuNeuron derivativeLeakyReLuNeuron = new DerivativeLeakyReLuNeuron();
            Action Initialise = () =>
            {
                derivativeLeakyReLuNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            DerivativeLeakyReLuNeuron derivativeLeakyReLuNeuron = new DerivativeLeakyReLuNeuron();
            derivativeLeakyReLuNeuron.InitialiseWithRandomValues(_numberOnInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeLeakyReLuNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
