

using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
    public class SeLuNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public SeLuNeuronTests()
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
            SeLuNeuron seLuNeuron = new SeLuNeuron();
            double outputOfTheNeuron = seLuNeuron.GetCalculationResult(_inputs);
                outputOfTheNeuron.Should().BeGreaterThan(-2);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            SeLuNeuron seLuNeuron = new SeLuNeuron();
            double outputOfTheNeuron = seLuNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = seLuNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }

    }
}
