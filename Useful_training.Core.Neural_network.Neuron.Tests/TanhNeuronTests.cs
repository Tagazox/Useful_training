



namespace Useful_training.Core.Neural_network.Neuron.Tests
{
    public class TanhNeuronTests
    {
        Random _rand;
        int _numberOnInputs;
        List<double> _inputs;
        public TanhNeuronTests()
        {
            _rand = new Random();
            _numberOnInputs = _rand.Next(1, 10);
            _inputs = new List<double>();
            for (int i = 0; i < _numberOnInputs; i++)
            {
                _inputs.Add(_rand.NextDouble() * 2 - 1);
            }
        }

        [Fact]
        public void NeuroneCalculationShouldBeOk()
        {
            TanhNeuron tanhNeuron = new TanhNeuron();
            tanhNeuron.InitialiseWithRandomValues(_numberOnInputs);
            double outputOfTheNeuron = tanhNeuron.GetCalculationResult(_inputs);
            outputOfTheNeuron.Should().BeInRange(-1, 1);
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            TanhNeuron tanhNeuron = new TanhNeuron();
            tanhNeuron.InitialiseWithRandomValues(_numberOnInputs);
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
            Calculate.Should().Throw<NeuronNotInitialisedException>();
        }

        [Fact]
        public void NeuroneCalculationShouldThrowCantInitialiseWithZeroInputException()
        {
            TanhNeuron tanhNeuron = new TanhNeuron();
            Action Initialise = () =>
            {
                tanhNeuron.InitialiseWithRandomValues(0);
            };
            Initialise.Should().Throw<CantInitialiseWithZeroInputException>();
        }
        [Fact]
        public void NeuroneCalculationShouldThrowWrongInputForCalculationException()
        {
            TanhNeuron tanhNeuron = new TanhNeuron();
            tanhNeuron.InitialiseWithRandomValues(_numberOnInputs);
            _inputs.RemoveAt(0);
            Action Calculate = () =>
            {
                double outputOfTheNeuron = tanhNeuron.GetCalculationResult(_inputs);
            };
            Calculate.Should().Throw<WrongInputForCalculationException>();
        }
    }
}
