

using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
    public class ReLuNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public ReLuNeuronTests()
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
            ReLuNeuron reLuNeuron = new ReLuNeuron();
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
            double outputOfTheNeuron = reLuNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = reLuNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }
    }
}
