using Moq;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.Neurons;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests;

public class NeuronsTests
{
	private uint _numberOfInputs;
	private double _inputsValue;
	private readonly IList<INeuron> _neuronsToTest;
	public NeuronsTests()
	{
		IList<IInputNeurons> inputsNeurons = MockInputNeuronsList();

		_neuronsToTest = new List<INeuron>();
		_neuronsToTest.Add(new EluNeuron(inputsNeurons));
		_neuronsToTest.Add(new LeakyReLuNeuron(inputsNeurons));
		_neuronsToTest.Add(new ReLuNeuron(inputsNeurons));
		_neuronsToTest.Add(new SeLuNeuron(inputsNeurons));
		_neuronsToTest.Add(new SigmoidNeuron(inputsNeurons));
		_neuronsToTest.Add(new SwishNeuron(inputsNeurons));
		_neuronsToTest.Add(new TanhNeuron(inputsNeurons));
	}
	private IList<IInputNeurons> MockInputNeuronsList()
	{
		IList<IInputNeurons> inputsNeurons = new List<IInputNeurons>();
		_numberOfInputs = 5;
		_inputsValue = 1;
		for (int i = 0; i < _numberOfInputs; i++)
		{
			Mock<IInputNeurons> mockedInputNeuron = new Mock<IInputNeurons>();
			mockedInputNeuron.Setup(inputNeurons => inputNeurons.OutputResult).Returns(_inputsValue);
			mockedInputNeuron.Setup(inputNeurons => inputNeurons.OutputSynapses).Returns(new List<Synapse>());
			inputsNeurons.Add(mockedInputNeuron.Object);
		}
		return inputsNeurons;
	}
	[Fact]
	public void NeuroneGetCalculationResultShouldBeOk()
	{
		foreach (INeuron neuron in _neuronsToTest)
		{
			neuron.GetCalculationResult();
			neuron.OutputResult.Should().BeInRange(-100,100);
		}
	}
	[Fact]
	public void NeuroneCalculateGradiantShouldBeOk()
	{
		foreach (INeuron neuron in _neuronsToTest)
		{
			neuron.GetCalculationResult();
			neuron.CalculateGradient(_inputsValue);
			neuron.Gradiant.Should().NotBe(0);
		}
	}
	[Fact]
	public void NeuroneCloneShouldBeOk()
	{
		foreach (INeuron neuron in _neuronsToTest)
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
		foreach (INeuron neuron in _neuronsToTest)
		{
			neuron.GetCalculationResult();
			double calculationResult = neuron.OutputResult;
			neuron.CalculateGradient(_inputsValue);
			neuron.UpdateWeights(1, 0.5);
			neuron.GetCalculationResult();
			neuron.OutputResult.Should().NotBe(calculationResult);
		}
	}
	[Fact]
	public void NeuroneUpdateWeightShouldThrowCantArgumentException()
	{
		foreach (INeuron neuron in _neuronsToTest)
		{
			neuron.GetCalculationResult();
			neuron.CalculateGradient(_inputsValue);
			Action updateWeightsCase1 = () =>
			{
				neuron.UpdateWeights(-1, 0.5);
			};
			Action updateWeightsCase2 = () =>
			{
				neuron.UpdateWeights(1, -0.5);
			};
			updateWeightsCase1.Should().Throw<ArgumentException>();
			updateWeightsCase2.Should().Throw<ArgumentException>();

		}
	}
	[Fact]
	public void NeuroneCreationShouldThrowCantInitializeWithZeroInputException()
	{
		// ReSharper disable once CollectionNeverUpdated.Local
		IList<IInputNeurons> badInputsNeurons = new List<IInputNeurons>();
		Action creationCase1 = () =>
		{
			INeuron unused = new EluNeuron(badInputsNeurons);
		};
		Action creationCase2 = () =>
		{
			INeuron unused = new LeakyReLuNeuron(badInputsNeurons);
		};
		Action creationCase3 = () =>
		{
			INeuron unused = new ReLuNeuron(badInputsNeurons);
		};
		Action creationCase4 = () =>
		{
			INeuron unused = new SeLuNeuron(badInputsNeurons);
		};
		Action creationCase5 = () =>
		{
			INeuron unused = new SigmoidNeuron(badInputsNeurons);
		};
		Action creationCase6 = () =>
		{
			INeuron unused = new SwishNeuron(badInputsNeurons);
		};
		Action creationCase7 = () =>
		{
			INeuron unused = new TanhNeuron(badInputsNeurons);
		};
		creationCase1.Should().Throw<CantInitializeWithZeroInputException>();
		creationCase2.Should().Throw<CantInitializeWithZeroInputException>();
		creationCase3.Should().Throw<CantInitializeWithZeroInputException>();
		creationCase4.Should().Throw<CantInitializeWithZeroInputException>();
		creationCase5.Should().Throw<CantInitializeWithZeroInputException>();
		creationCase6.Should().Throw<CantInitializeWithZeroInputException>();
		creationCase7.Should().Throw<CantInitializeWithZeroInputException>();
	}
}