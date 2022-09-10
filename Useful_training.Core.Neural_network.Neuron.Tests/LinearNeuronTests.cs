

using Useful_training.Core.Neural_network.Neuron.Exceptions;

namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class LinearNeuronTests
    {
        Random _rand;
        int _numberOfInputs;
        List<double> _inputs;
        public LinearNeuronTests()
        {
            _rand = new Random();
            _numberOfInputs = _rand.Next(1, 10);
            _inputs = new List<double>();
            for (int i = 0; i < _numberOfInputs; i++)
            {
                _inputs.Add(_rand.NextDouble() * 2 - 1);
            }
        }

        [Fact]
        public void NeuroneCalculationShouldBeOk()
        {
            LinearNeuron linearNeuron = new LinearNeuron();
            linearNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = linearNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeOfType(typeof(double));
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            LinearNeuron linearNeuron = new LinearNeuron();
            linearNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = linearNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = linearNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            LinearNeuron linearNeuron = new LinearNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = linearNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            LinearNeuron linearNeuron = new LinearNeuron();
            Action Initialise = () =>
            {
                linearNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            LinearNeuron linearNeuron = new LinearNeuron();
            linearNeuron.InitialiseWithRandomValues(_numberOfInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = linearNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
