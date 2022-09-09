

using Useful_training.Core.Neural_network.Neuron.Exceptions;

namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class SeLuNeuronTests
    {
        Random _rand;
        int _numberOnInputs;
        List<double> _inputs;
        public SeLuNeuronTests()
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
            SeLuNeuron seLuNeuron = new SeLuNeuron();
            seLuNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = seLuNeuron.GetCalculationResult(_inputs);
            if (outputOfTheNeuron > 0)
                outputOfTheNeuron.Should().BeOfType(typeof(double));
            else
                outputOfTheNeuron.Should().Be(0);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            SeLuNeuron seLuNeuron = new SeLuNeuron();
            seLuNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = seLuNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = seLuNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }

        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            SeLuNeuron seLuNeuron = new SeLuNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = seLuNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            SeLuNeuron seLuNeuron = new SeLuNeuron();
            Action Initialise = () =>
            {
                seLuNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            SeLuNeuron seLuNeuron = new SeLuNeuron();
            seLuNeuron.InitialiseWithRandomValues(_numberOnInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = seLuNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
