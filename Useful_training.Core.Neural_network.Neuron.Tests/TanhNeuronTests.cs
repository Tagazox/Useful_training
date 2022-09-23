



namespace Useful_training.Core.Neural_network.Tests
{
    public class TanhNeuronTests
    {
        Random _rand;
        uint _numberOfInputs;
        List<double> _inputs;
        public TanhNeuronTests()
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
            TanhNeuron tanhNeuron = new TanhNeuron();
            double outputOfTheNeuron = tanhNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(-1, 1);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            TanhNeuron tanhNeuron = new TanhNeuron();
            double outputOfTheNeuron = tanhNeuron.GetCalculationResult(_inputs);

            INeuron cloneNeuron = tanhNeuron.Clone();
            double outputOfTheClonedNeuron = cloneNeuron.GetCalculationResult(_inputs);
            outputOfTheClonedNeuron.Should().Be(outputOfTheNeuron);

        }

        [Fact]
        public void NeuroneCalculationShouldThrowNeuronNotInitialisedException()
        {
            TanhNeuron tanhNeuron = new TanhNeuron();
            Action Calculate = () =>
            {
                double outputOfTheNeuron = tanhNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<CantCalculateWithInputNeurons>();
        }

      
    }
}
