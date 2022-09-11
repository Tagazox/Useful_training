
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
    public class DerivativeSigmoidNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public DerivativeSigmoidNeuronTests()
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
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            derivativeSigmoidNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = derivativeSigmoidNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(0, 0.3);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            derivativeSigmoidNeuron.InitialiseWithRandomValues(_numberOfInputs);
            double outputOfTheNeuron = derivativeSigmoidNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = derivativeSigmoidNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeSigmoidNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneInitialisationShouldThrowCantInitializeWithZeroInputException()
        {
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            Action Calculate = () =>
            {
                derivativeSigmoidNeuron.InitialiseWithRandomValues(0);
            };
            Calculate.Should().Throw<CantInitializeWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            DerivativeSigmoidNeuron derivativeSigmoidNeuron = new DerivativeSigmoidNeuron();
            derivativeSigmoidNeuron.InitialiseWithRandomValues(_numberOfInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = derivativeSigmoidNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
