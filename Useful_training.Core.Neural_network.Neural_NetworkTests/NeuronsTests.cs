using Moq;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Neurons;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests
{
	public class NeuronsTests
	{
		uint NumberOfInputs;
		double Inputsvalue;
		IList<INeuron> NeuronsToTest;
		public NeuronsTests()
		{
			IList<IInputNeurons> InputsNeurons = MockInputNeuronsList();

			NeuronsToTest = new List<INeuron>();
			NeuronsToTest.Add(new EluNeuron(InputsNeurons));
			NeuronsToTest.Add(new LeakyReLuNeuron(InputsNeurons));
			NeuronsToTest.Add(new ReLuNeuron(InputsNeurons));
			NeuronsToTest.Add(new SeLuNeuron(InputsNeurons));
			NeuronsToTest.Add(new SigmoidNeuron(InputsNeurons));
			NeuronsToTest.Add(new SwishNeuron(InputsNeurons));
			NeuronsToTest.Add(new TanhNeuron(InputsNeurons));
		}
		private IList<IInputNeurons> MockInputNeuronsList()
		{
			IList<IInputNeurons> InputsNeurons = new List<IInputNeurons>();
			NumberOfInputs = 5;
			Inputsvalue = 1;
			for (int i = 0; i < NumberOfInputs; i++)
			{
				Mock<IInputNeurons> mockedInputNeuron = new Mock<IInputNeurons>();
				mockedInputNeuron.Setup(i => i.OutputResult).Returns(Inputsvalue);
				mockedInputNeuron.Setup(i => i.OutputSynapses).Returns(new List<Synapse>());
				InputsNeurons.Add(mockedInputNeuron.Object);
			}
			return InputsNeurons;
		}
		[Fact]
		public void NeuroneGetCalculationResultShouldBeOk()
		{
			foreach (INeuron neuron in NeuronsToTest)
			{
				neuron.GetCalculationResult();
				neuron.OutputResult.Should().NotBe(0);
			}
		}
		[Fact]
		public void NeuroneCalculateGradiantShouldBeOk()
		{
			foreach (INeuron neuron in NeuronsToTest)
			{
				neuron.GetCalculationResult();
				neuron.CalculateGradient(Inputsvalue);
				neuron.Gradiant.Should().NotBe(0);
			}
		}
		[Fact]
		public void NeuroneCloneShouldBeOk()
		{
			foreach (INeuron neuron in NeuronsToTest)
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
			foreach (INeuron neuron in NeuronsToTest)
			{
				neuron.GetCalculationResult();
				double calculationResult = neuron.OutputResult;
				neuron.CalculateGradient(Inputsvalue);
				neuron.UpdateWeights(1, 0.5);
				neuron.GetCalculationResult();
				neuron.OutputResult.Should().NotBe(calculationResult);
			}
		}
		[Fact]
		public void NeuroneUpdateWeightShouldThrowCantArgumentException()
		{
			foreach (INeuron neuron in NeuronsToTest)
			{
				neuron.GetCalculationResult();
				double calculationResult = neuron.OutputResult;
				neuron.CalculateGradient(Inputsvalue);
				Action UpdateWeightsCase1 = () =>
				{
					neuron.UpdateWeights(-1, 0.5);
				};
				Action UpdateWeightsCase2 = () =>
				{
					neuron.UpdateWeights(1, -0.5);
				};
				UpdateWeightsCase1.Should().Throw<ArgumentException>();
				UpdateWeightsCase2.Should().Throw<ArgumentException>();

			}
		}
		[Fact]
		public void NeuroneCreationShouldThrowCantInitializeWithZeroInputException()
		{
			IList<IInputNeurons> InputsNeurons = new List<IInputNeurons>();
			Action CreationCase1 = () =>
			{
				INeuron neuron = new EluNeuron(InputsNeurons);
			};
			Action CreationCase2 = () =>
			{
				INeuron neuron = new LeakyReLuNeuron(InputsNeurons);
			};
			Action CreationCase3 = () =>
			{
				INeuron neuron = new ReLuNeuron(InputsNeurons);
			};
			Action CreationCase4 = () =>
			{
				INeuron neuron = new SeLuNeuron(InputsNeurons);
			};
			Action CreationCase5 = () =>
			{
				INeuron neuron = new SigmoidNeuron(InputsNeurons);
			};
			Action CreationCase6 = () =>
			{
				INeuron neuron = new SwishNeuron(InputsNeurons);
			};
			Action CreationCase7 = () =>
			{
				INeuron neuron = new TanhNeuron(InputsNeurons);
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
