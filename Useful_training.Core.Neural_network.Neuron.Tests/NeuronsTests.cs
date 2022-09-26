
using Useful_training.Core.Neural_network.Exceptions;

namespace Useful_training.Core.Neural_network.Tests
{
    public class NeuronsTests
    {
        Random _rand;
        uint _numberOfInputs;
        IList<IInputNeurons> InputNeurons;
        IList<INeuron> neuronsToTest;
        public NeuronsTests()
        {
            _rand = new Random();
            _numberOfInputs = 5;
            InputNeurons = new List<IInputNeurons>();
            for (int i = 0; i < _numberOfInputs; i++)
            {
                InputNeuron InputNeuron = new InputNeuron();
                InputNeuron.OutputResult = 1;
                InputNeurons.Add(InputNeuron);
            }
            neuronsToTest = new List<INeuron>(); 
            neuronsToTest.Add(new EluNeuron(InputNeurons));
            neuronsToTest.Add(new LeakyReLuNeuron(InputNeurons));
            neuronsToTest.Add(new ReLuNeuron(InputNeurons));
            neuronsToTest.Add(new SeLuNeuron(InputNeurons));
            neuronsToTest.Add(new SigmoidNeuron(InputNeurons));
            neuronsToTest.Add(new SwishNeuron(InputNeurons));
            neuronsToTest.Add(new TanhNeuron(InputNeurons));
        }
        [Fact]
        public void NeuroneGetCalculationResultShouldBeOk()
        {
            foreach (INeuron neuron in neuronsToTest)
            {
                neuron.GetCalculationResult();
                neuron.OutputResult.Should().NotBe(0);
            }
        }

    }
}
