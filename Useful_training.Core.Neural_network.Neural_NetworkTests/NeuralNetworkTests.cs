using Newtonsoft.Json;
using Useful_training.Core.NeuralNetwork.Exceptions;
using Useful_training.Core.NeuralNetwork.NeuralNetwork.Interfaces;
using Useful_training.Core.NeuralNetwork.Neurons.Type.Enums;

namespace Useful_training.Core.NeuralNetwork.NeuralNetwork.Tests;

public class NeuralNetworkTests
{
    private readonly NeuralNetwork _testSubject;
    private readonly NeuralNetwork _initializedTestSubject;
    private readonly IEnumerable<NeuronType> _neuronTypeAvailable;
    private List<double> _input;
    private readonly Random _rand;
    public NeuralNetworkTests()
    {
        _neuronTypeAvailable = Enum.GetValues(typeof(NeuronType)).Cast<NeuronType>();
        _rand = new Random();
        _input = new List<double>();
        _testSubject = new NeuralNetwork();
        _initializedTestSubject = new NeuralNetwork();
        int inputNumber = _rand.Next(5,30);
        for (int i = 0; i < _rand.Next(); i++)
        {
            _input.Add(_rand.Next() * 2 - 1);
        }
        _initializedTestSubject.Initialize((uint)_input.Count, null, null);

        foreach (NeuronType type in _neuronTypeAvailable)
        {
            _initializedTestSubject.AddHiddenLayer((uint)_rand.Next(1, 10), type);
        }
    }
    [Fact]
    public void NeuralNetworkInitializeShouldBeGood()
    {
        _testSubject.Initialize((uint)_input.Count, null, null);
        _testSubject.Initialize((uint)_input.Count, .4, .5);
    }
    [Fact]
    public void NeuralNetworkShouldThrowArgumentException()
    {
        Action initializeBadLearnRate = () =>
        {
            _testSubject.Initialize(2, -.5, .5);
        };
        Action initializeBadMomentum = () =>
        {
            _testSubject.Initialize(2, .5, -.5);
        };
        initializeBadLearnRate.Should().Throw<ArgumentException>();
        initializeBadMomentum.Should().Throw<ArgumentException>();
    }
    [Fact]
    public void NeuralNetworkShouldBeResetGood()
    {
        IList<double> result = _initializedTestSubject.Calculate(_input);
        _initializedTestSubject.Reset();
        _initializedTestSubject.Calculate(_input).Should().NotBeSameAs(result);
    }
    [Fact]
    public void NeuralNetworkAddHiddenLayerShouldBeGood()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            _initializedTestSubject.AddHiddenLayer((uint)_rand.Next(1, 20), type);
        }
    }
    [Fact]
    public void NeuralNetworkAddHiddenLayerShouldThrowNeedToBeCreatedByTheBuilderException()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            Action addHiddenLayer = () =>
            {
                _testSubject.AddHiddenLayer(0, type);
            };
            addHiddenLayer.Should().Throw<NeedToBeCreatedByTheBuilderException>();
        }
    }
    [Fact]
    public void NeuralNetworkAddHiddenLayerShouldThrowCantInitializeWithZeroNeuronException()
    {
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            Action addHiddenLayer = () =>
            {
                _initializedTestSubject.AddHiddenLayer(0, type);
            };
            addHiddenLayer.Should().Throw<CantInitializeWithZeroNeuronException>();
        }
    }
    [Fact]
    public IList<double> NeuralNetworkCalculateShouldBeGood()
    {
        uint lastLayerCount = 0;
        _testSubject.Initialize((uint)_input.Count, null, null);
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            lastLayerCount = (uint)_rand.Next(1, 10);
            _testSubject.AddHiddenLayer(lastLayerCount, type);
        }
        IList<double> result = _testSubject.Calculate(_input);
        result.Count.Should().Be((int)lastLayerCount);
        return result;
    }
    [Fact]
    public void NeuralNetworkCalculateShouldThrowWrongInputForCalculationException()
    {
        _testSubject.Initialize((uint)_input.Count, null, null);
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            uint lastLayerCount = (uint)_rand.Next(1, 10);
            _testSubject.AddHiddenLayer(lastLayerCount, type);
        }


        Action calculateCase1 = () =>
        {
            _input = new List<double> { 1 };
            _testSubject.Calculate(_input);
        };
        Action calculateCase2 = () =>
        {
            _input = new List<double> { 1, 0, 0 };
            _testSubject.Calculate(_input);
        };
        calculateCase1.Should().Throw<WrongInputForCalculationException>();
        calculateCase2.Should().Throw<WrongInputForCalculationException>();
    }
    [Fact]
    public void NeuralNetworkBackPropagateShouldBeGood()
    {
        uint lastLayerCount = 0;
        _testSubject.Initialize((uint)_input.Count, null, null);
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            lastLayerCount = (uint)_rand.Next(1, 10);
            _testSubject.AddHiddenLayer(lastLayerCount, type);
        }
        List<double> targets = new List<double>();
        for (int i = 0; i < lastLayerCount; i++)
        {
            targets.Add(1);
        }
        IList<double> output = _testSubject.Calculate(_input);
        ((INeuralNetwork)_testSubject).BackPropagate(targets);
        _testSubject.Calculate(_input).Should().NotBeSameAs(output);
    }
    [Fact]
    public void NeuralNetworkBackPropagateShouldThrowArgumentException()
    {
        uint lastLayerCount = 0;
        _testSubject.Initialize((uint)_input.Count, null, null);
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            lastLayerCount = (uint)_rand.Next(1, 10);
            _testSubject.AddHiddenLayer(lastLayerCount, type);
        }
        List<double> targets = new List<double>();
        for (int i = 0; i < lastLayerCount - 1; i++)
        {
            targets.Add(1);
        }
        Action backPropagateCase1 = () =>
        {
            ((INeuralNetwork)_testSubject).BackPropagate(targets);
        };
        backPropagateCase1.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void NeuralNetworkSerialisationShouldBeGood()
    {
        uint lastLayerCount = 0;
        _testSubject.Initialize((uint)_input.Count, null, null);
        foreach (NeuronType type in _neuronTypeAvailable)
        {
            lastLayerCount = (uint)_rand.Next(1, 10);
            _testSubject.AddHiddenLayer(lastLayerCount, type);
        }
        List<double> targets = new List<double>();
        for (int i = 0; i < lastLayerCount; i++)
        {
            targets.Add(1);
        }
        ((INeuralNetwork)_testSubject).BackPropagate(targets);
        string json = JsonConvert.SerializeObject(_testSubject, Formatting.Indented);
        INeuralNetwork neuralNetworkDeserialized = JsonConvert.DeserializeObject<NeuralNetwork>(json) ?? throw new InvalidOperationException();

        IList<double> resultsOfDeserializedNetwork = neuralNetworkDeserialized.Calculate(_input);
        IList<double> resultsOfTestSubject = _testSubject.Calculate(_input);

        resultsOfDeserializedNetwork.Should().BeEquivalentTo(resultsOfTestSubject);

        ((INeuralNetwork)_testSubject).BackPropagate(targets);
        neuralNetworkDeserialized.BackPropagate(targets);

        resultsOfDeserializedNetwork = neuralNetworkDeserialized.Calculate(_input);
        resultsOfTestSubject = _testSubject.Calculate(_input);

        resultsOfDeserializedNetwork.Should().BeEquivalentTo(resultsOfTestSubject);

    }
}