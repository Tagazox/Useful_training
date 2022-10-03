
using Useful_training.Core.Neural_network.Exceptions;
using Useful_training.Core.Neural_network.Interface;
using Useful_training.Core.Neural_network.Neurons;

namespace Useful_training.Core.Neural_network.Neural_NetworkTests
{
    public class NeuronsTests
    {
        Random _rand;
        uint _numberOfInputs;
        IList<IInputNeurons> InputNeurons;
        double inputsvalue;
        IList<INeuron> neuronsToTest;
        public NeuronsTests()
        {
            _rand = new Random();
            _numberOfInputs = 5;
            InputNeurons = new List<IInputNeurons>();
            inputsvalue = 1;
            for (int i = 0; i < _numberOfInputs; i++)
            {
                InputNeuron InputNeuron = new InputNeuron();
                InputNeuron.OutputResult = inputsvalue;
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
        [Fact]
        public void NeuroneCalculateGradiantShouldBeOk()
        {
            foreach (INeuron neuron in neuronsToTest)
            {
                neuron.GetCalculationResult();
                neuron.CalculateGradient(inputsvalue);
                neuron.Gradiant.Should().NotBe(0);
            }
        }
        [Fact]
        public void NeuroneCloneShouldBeOk()
        {
            foreach (INeuron neuron in neuronsToTest)
            {
                neuron.GetCalculationResult();
                INeuron neuronCloned = neuron.Clone();
                neuronCloned.GetCalculationResult();
                neuron.OutputResult.Should().Be(neuronCloned.OutputResult);
            }
        }
        [Fact]
        public void NeuroneUpdateWeightShouldBeOk()
        {
            foreach (INeuron neuron in neuronsToTest)
            {
                neuron.GetCalculationResult();
                double calculationResult = neuron.OutputResult;
                neuron.CalculateGradient(inputsvalue);
                neuron.UpdateWeights(1, 0.5);
                neuron.GetCalculationResult();
                neuron.OutputResult.Should().NotBe(calculationResult);
            }
        }

        [Fact]
        public void NeuroneCreationShouldThrowCantInitializeWithZeroInputException()
        {
            InputNeurons = new List<IInputNeurons>();
            Action CreationCase1 = () =>
            {
                INeuron neuron = new EluNeuron(InputNeurons);
            };
            Action CreationCase2 = () =>
            {
                INeuron neuron = new LeakyReLuNeuron(InputNeurons);
            };
            Action CreationCase3 = () =>
            {
                INeuron neuron = new ReLuNeuron(InputNeurons);
            };
            Action CreationCase4 = () =>
            {
                INeuron neuron = new SeLuNeuron(InputNeurons);
            };
            Action CreationCase5 = () =>
            {
                INeuron neuron = new SigmoidNeuron(InputNeurons);
            };
            Action CreationCase6 = () =>
            {
                INeuron neuron = new SwishNeuron(InputNeurons);
            };
            Action CreationCase7 = () =>
            {
                INeuron neuron = new TanhNeuron(InputNeurons);
            };
            CreationCase1.Should().Throw<CantInitializeWithZeroInputException>();
            CreationCase2.Should().Throw<CantInitializeWithZeroInputException>();
            CreationCase3.Should().Throw<CantInitializeWithZeroInputException>();
            CreationCase4.Should().Throw<CantInitializeWithZeroInputException>();
            CreationCase5.Should().Throw<CantInitializeWithZeroInputException>();
            CreationCase6.Should().Throw<CantInitializeWithZeroInputException>();
            CreationCase7.Should().Throw<CantInitializeWithZeroInputException>();
        }
    }
}
