
using Useful_training.Core.Neural_network.Neuron.Exceptions;

namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class EluNeuronTests
    {
        Random _rand;
        int _numberOnInputs;
        List<double> _inputs;
        public EluNeuronTests()
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
            EluNeuron eluNeuron = new EluNeuron();
            eluNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = eluNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(-1, double.MaxValue);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            EluNeuron eluNeuron = new EluNeuron();
            eluNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = eluNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = eluNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            EluNeuron eluNeuron = new EluNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = eluNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            EluNeuron eluNeuron = new EluNeuron();
            Action Initialise = () =>
            {
                eluNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            EluNeuron eluNeuron = new EluNeuron();
            eluNeuron.InitialiseWithRandomValues(_numberOnInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = eluNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
