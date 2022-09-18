

using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
    public class SwishNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public SwishNeuronTests()
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
            SwishNeuron swishNeuron = new SwishNeuron();
            swishNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = swishNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeOfType(typeof(double));

        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            SwishNeuron swishNeuron = new SwishNeuron();
            swishNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = swishNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = swishNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);

        }


        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            SwishNeuron swishNeuron = new SwishNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = swishNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneInitialisationShouldThrowCantInitializeWithZeroInputException()
        {
            SwishNeuron swishNeuron = new SwishNeuron();
            Action Initialise = () =>
            {
                swishNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            SwishNeuron swishNeuron = new SwishNeuron();
            swishNeuron.InitialiseWithRandomValues(_numberOfInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = swishNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
