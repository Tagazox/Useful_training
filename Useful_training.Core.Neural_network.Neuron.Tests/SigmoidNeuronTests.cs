

using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
    public class SigmoidNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public SigmoidNeuronTests()
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
            SigmoidNeuron sigmoidNeuron = new SigmoidNeuron();
            double outputOfTheNeuron = sigmoidNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(0, 1);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            SigmoidNeuron sigmoidNeuron = new SigmoidNeuron();
            double outputOfTheNeuron = sigmoidNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = sigmoidNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);
        }

       
    }
}
