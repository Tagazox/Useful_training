

using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
    public class GeLuNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public GeLuNeuronTests()
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
            GeLuNeuron geLuNeuron = new GeLuNeuron();
            geLuNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = geLuNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(-0.2, 2);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            GeLuNeuron geLuNeuron = new GeLuNeuron();
            geLuNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = geLuNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = geLuNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            GeLuNeuron geLuNeuron = new GeLuNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = geLuNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneInitialisationShouldThrowCantInitializeWithZeroInputException()
        {
            GeLuNeuron geLuNeuron = new GeLuNeuron();
            Action Initialise = () =>
            {
                geLuNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            GeLuNeuron geLuNeuron = new GeLuNeuron();
            geLuNeuron.InitialiseWithRandomValues(_numberOfInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = geLuNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
