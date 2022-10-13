using Moq;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones;
using Useful_training.Core.NeuralNetwork.LayersOfNeurones.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons;
using Useful_training.Core.NeuralNetwork.Neurons.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests;

public class LayerOfNeuronsTests
{
    private readonly IEnumerable<NeuronType> _neuronTypeAvailable;
    private readonly ILayerOfNeurons _testLayerOfNeurons;
    private readonly Mock<ILayerOfInputNeurons> _layerOfInputNeuronesMocked;
    private readonly uint _numberOfNeurons;
    private List<double>? _targets;

    public LayerOfNeuronsTests()
    {
        Random rand = new Random();
        _numberOfNeurons = (uint)rand.Next(1, 5);

        _layerOfInputNeuronesMocked = new Mock<ILayerOfInputNeurons>();
        _layerOfInputNeuronesMocked.Setup(s => s.InputsNeurons).Returns(MockInputNeuronsList());
            
        _neuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
        _testLayerOfNeurons = new LayerOfNeurons();
    }
    private static IList<IInputNeuron> MockInputNeuronsList()
    {
        IList<IInputNeuron> inputsNeurons = new List<IInputNeuron>();
        const int numberOfInputs = 5;
        const double inputsValue = 1;
        for (int i = 0; i < numberOfInputs; i++)
        {
            Mock<IInputNeuron> mockedInputNeuron = new Mock<IInputNeuron>();
            mockedInputNeuron.Setup(inputNeurons => inputNeurons.OutputResult).Returns(inputsValue);
            mockedInputNeuron.Setup(inputNeurons => inputNeurons.OutputSynapses).Returns(new List<Synapse>());
            inputsNeurons.Add(mockedInputNeuron.Object);
        }
        return inputsNeurons;
    }
    [Fact]
    public void LayerOfNeuronsInitializeShouldBeGoodWithAllNeuroneType()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            _testLayerOfNeurons.Initialize(_numberOfNeurons, type, _layerOfInputNeuronesMocked.Object);
        }
    }
    [Fact]
    public void LayerOfNeuronsInitializeShouldThrowCantInitializeWithZeroNeuronException()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            Action creationCase1 = () =>
            {
                _testLayerOfNeurons.Initialize(0, type, _layerOfInputNeuronesMocked.Object);
            };
            creationCase1.Should().Throw<CantInitializeWithZeroNeuronException>();
        }
    }
    [Fact]
    public void LayerOfNeuronsInitializeShouldThrowNeuronTypeDontExistsException()
    {
        Action creationCase1 = () =>
        {
            _testLayerOfNeurons.Initialize(_numberOfNeurons, (NeuronType)(_neuronTypeAvailable.Count() + 1), _layerOfInputNeuronesMocked.Object);
        };
        if (creationCase1 == null) throw new ArgumentNullException(nameof(creationCase1));
        creationCase1.Should().Throw<NeuronTypeDontExistsException>();
    }
    [Fact]
    public void LayerOfNeuronsInitializeShouldThrowBadInputLayerException()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            _layerOfInputNeuronesMocked.Setup(s => s.InputsNeurons).Returns(new List<IInputNeuron>());

            Action creationCase1 = () =>
            {
                _testLayerOfNeurons.Initialize(_numberOfNeurons, type, _layerOfInputNeuronesMocked.Object);
            };
            Action creationCase2 = () =>
            {
                _testLayerOfNeurons.Initialize(_numberOfNeurons, type, null!);
            };
            creationCase1.Should().Throw<BadInputLayerException>();
            creationCase2.Should().Throw<BadInputLayerException>();
        }
    }
    [Fact]
    public void LayerOfNeuronsCalculateShouldBeGood()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            _testLayerOfNeurons.Initialize(_numberOfNeurons, type, _layerOfInputNeuronesMocked.Object);
            _testLayerOfNeurons.Calculate();
            foreach (double output in _testLayerOfNeurons.Outputs)
            {
                output.Should().BeOfType(typeof(double));
            }
        }
    }
    [Fact]
    public void LayerOfNeuronsCalculateGradiantShouldBeGood()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            _testLayerOfNeurons.Initialize(_numberOfNeurons, type, _layerOfInputNeuronesMocked.Object);
            _testLayerOfNeurons.Calculate();
            _testLayerOfNeurons.CalculateGradiant(_targets);
            foreach (INeuron neuron in _testLayerOfNeurons.Neurons)
            {
                neuron.Gradiant.Should().BeOfType(typeof(double));
            }
        }
    }
    [Fact]
    public void LayerOfNeuronsCalculateGradiantShouldThrowArgumentException()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            _testLayerOfNeurons.Initialize(_numberOfNeurons, type, _layerOfInputNeuronesMocked.Object);
            _testLayerOfNeurons.Calculate();
            _targets = new List<double>();
            Action calculateGradiant = () =>
            {
                _testLayerOfNeurons.CalculateGradiant(_targets);
            };
            if (calculateGradiant == null) throw new ArgumentNullException(nameof(calculateGradiant));
            calculateGradiant.Should().Throw<ArgumentException>();
        }
    }
    [Fact]
    public void LayerOfNeuronsUpdateWeightShouldBeGood()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            _testLayerOfNeurons.Initialize(_numberOfNeurons, type, _layerOfInputNeuronesMocked.Object);
            _testLayerOfNeurons.Calculate();
            IList<double> outputs = _testLayerOfNeurons.Outputs;
            _testLayerOfNeurons.CalculateGradiant(_targets);
            _testLayerOfNeurons.UpdateWeights(.5, .5);
            _testLayerOfNeurons.Calculate();
            _testLayerOfNeurons.Outputs.Should().NotBeSameAs(outputs);
        }
    }
       
}