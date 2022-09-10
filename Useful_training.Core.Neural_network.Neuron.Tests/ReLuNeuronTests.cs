

using Useful_training.Core.Neural_network.Neuron.Exceptions;

namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class ReLuNeuronTests
    {
        Random _rand;
        int _numberOfInputs;
        List<double> _inputs;
        public ReLuNeuronTests()
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
            ReLuNeuron reLuNeuron = new ReLuNeuron();
            reLuNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = reLuNeuron.GetCalculationResult(_inputs);
            if (outputOfTheNeuron > 0)
                outputOfTheNeuron.Should().BeOfType(typeof(double));
            else
                outputOfTheNeuron.Should().Be(0);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            ReLuNeuron reLuNeuron = new ReLuNeuron();
            reLuNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = reLuNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = reLuNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            ReLuNeuron reLuNeuron = new ReLuNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = reLuNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            ReLuNeuron reLuNeuron = new ReLuNeuron();
            Action Initialise = () =>
            {
                reLuNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            ReLuNeuron reLuNeuron = new ReLuNeuron();
            reLuNeuron.InitialiseWithRandomValues(_numberOfInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = reLuNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
