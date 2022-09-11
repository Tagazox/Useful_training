
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
    public class DerivativeTanhNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public DerivativeTanhNeuronTests()
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
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            derivativeTanhNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = derivativeTanhNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(0, 1);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            derivativeTanhNeuron.InitialiseWithRandomValues(_numberOfInputs);
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
        public void NeuroneInitialisationShouldThrowCantInitializeWithZeroInputException()
        {
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            Action Initialise = () =>
            {
                derivativeTanhNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            DerivativeTanhNeuron derivativeTanhNeuron = new DerivativeTanhNeuron();
            derivativeTanhNeuron.InitialiseWithRandomValues(_numberOfInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeTanhNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
